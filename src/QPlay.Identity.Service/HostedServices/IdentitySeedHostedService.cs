using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QPlay.Identity.Service.Constants;
using QPlay.Identity.Service.Models.Entities;
using QPlay.Identity.Service.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace QPlay.Identity.Service.HostedServices;

public class IdentitySeedHostedService : IHostedService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly IdentitySettings identitySettings;

    public IdentitySeedHostedService(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<IdentitySettings> identityOptions
    )
    {
        this.serviceScopeFactory = serviceScopeFactory;
        identitySettings = identityOptions.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        RoleManager<ApplicationRole> roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<ApplicationRole>>();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        await CreateRoleIfNotExistsAsync(Roles.ADMIN, roleManager);
        await CreateRoleIfNotExistsAsync(Roles.PLAYER, roleManager);

        ApplicationUser adminUser = await userManager.FindByEmailAsync(
            identitySettings.AdminUserEmail
        );

        if (adminUser == null)
        {
            adminUser = new()
            {
                UserName = identitySettings.AdminUserEmail,
                Email = identitySettings.AdminUserEmail
            };

            await userManager.CreateAsync(adminUser, identitySettings.AdminUserPassword);
            await userManager.AddToRoleAsync(adminUser, Roles.ADMIN);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task CreateRoleIfNotExistsAsync(
        string roleName,
        RoleManager<ApplicationRole> roleManager
    )
    {
        bool roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new() { Name = roleName });
        }
    }
}
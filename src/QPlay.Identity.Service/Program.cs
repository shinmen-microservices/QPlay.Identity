using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QPlay.Identity.Service.Extensions;
using QPlay.Identity.Service.HostedServices;

namespace QPlay.Identity.Service;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.ConfigureApplicationUser(builder.Configuration);
        builder.Services.ConfigureIdentityServer(builder.Configuration);
        builder.Services.ConfigureMassTransit();
        builder.Services.AddControllers();
        builder.Services.AddHostedService<IdentitySeedHostedService>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ConfigureCors(builder.Configuration);
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapControllers();
        app.MapRazorPages();
        app.Run();
    }
}
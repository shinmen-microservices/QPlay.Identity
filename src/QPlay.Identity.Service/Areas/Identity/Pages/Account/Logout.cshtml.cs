using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QPlay.Identity.Service.Models.Entities;
using System.Threading.Tasks;

namespace QPlay.Identity.Service.Areas.Identity.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;
    private readonly IIdentityServerInteractionService _identityServerInteractionService;

    public LogoutModel(
        SignInManager<ApplicationUser> signInManager,
        ILogger<LogoutModel> logger,
        IIdentityServerInteractionService identityServerInteractionService
    )
    {
        _signInManager = signInManager;
        _logger = logger;
        _identityServerInteractionService = identityServerInteractionService;
    }

    public async Task<IActionResult> OnGet(string logoutId)
    {
        LogoutRequest logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(
            logoutId
        );
        if (logoutRequest?.ShowSignoutPrompt == false)
        {
            return await OnPost(logoutRequest.PostLogoutRedirectUri);
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        if (returnUrl != null)
        {
            return Redirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToPage();
        }
    }
}
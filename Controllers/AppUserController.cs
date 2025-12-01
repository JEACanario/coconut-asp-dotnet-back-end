using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;

[ApiController]
[Route("[controller]")]
public class AppUserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AppUserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] AppUser model, string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var result = IdentityResult.Failed();

        if (user != null)
            result = await _userManager.UpdateAsync(model);

        if (result.Succeeded)
        {
            // Your registration success logic
            return Ok(new { Message = "Registration successful" });
        }

        // If registration fails, return errors
        return BadRequest(new { Errors = result.Errors });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        // Sign out of the Identity cookie scheme and external scheme
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // Best-effort cookie removal (default Identity cookie name)
        HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");

        return NoContent();
    }
}

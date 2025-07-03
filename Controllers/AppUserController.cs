using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

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
}

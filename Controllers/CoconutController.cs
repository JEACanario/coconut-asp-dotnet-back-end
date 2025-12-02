using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CoconutController : ControllerBase
{
    private readonly ILogger<CoconutController> _logger;
    private readonly CoconutContext _dbcontext;
    private readonly UserManager<AppUser> _userManager;

    public CoconutController(
        ILogger<CoconutController> logger,
        CoconutContext coconutContext,
        UserManager<AppUser> userManager
    )
    {
        _logger = logger;
        _dbcontext = coconutContext;
        _userManager = userManager;
    }

    [HttpGet("{id}")]
    public ActionResult<Coconut> Get(int id)
    {
        var coconuts = _dbcontext.Coconuts;
        var coconut = coconuts.Find(id);

        if (coconut == null)
            return NotFound();

        return coconut;
    }

    [HttpGet(Name = "GetCoconuts")]
    public async Task<IEnumerable<Coconut>> Get()
    {
        var coconuts = _dbcontext.Coconuts;

        var request = HttpContext.Request;
        var user = await _userManager.GetUserAsync(HttpContext.User);
        //Debugging info to trace requests:
        Console.WriteLine($"Request by user: {HttpContext.User.Identity?.Name ?? "Anonymous"}");
        Console.WriteLine("Request Information:");
        Console.WriteLine($"Request Method: {request.Method}");
        Console.WriteLine($"Request Path: {request.Path}");
        Console.WriteLine($"Request Query String: {request.QueryString}");
        Console.WriteLine(
            $"Request Headers: {string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"))}"
        );
        Console.WriteLine($"Request Body: {request.Body}");
        //End Debugging info
        if (user == null)
            return Enumerable.Empty<Coconut>();
        else
            return coconuts.Where(c => c.UserId == user.Id);
    }

    [HttpPost(Name = "CreateCoconut")]
    public IActionResult Create(Coconut coconut)
    {
        async Task<AppUser?> getUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user;
        }

        var coconuts = _dbcontext.Coconuts;
        var user = getUser().Result;

        if (user == null)
            return Unauthorized();
        var userId = user.Id;

        coconut.UserId = userId;
        coconut.User = user;

        coconuts.Add(coconut);
        _dbcontext.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = coconut.Id }, coconut);
    }

    [HttpPut("{id}", Name = "UpdateCoconut")]
    public IActionResult Update(int id, Coconut coconut)
    {
        if (id != coconut.Id)
            return BadRequest();

        var coconuts = _dbcontext.Coconuts;
        var existingCoconut = coconuts.Find(id);
        if (existingCoconut is null)
            return NotFound();

        if (coconuts.Entry(existingCoconut) is null)
        {
            coconuts.Update(coconut);
        }
        else
        {
            coconuts.Entry(existingCoconut).CurrentValues.SetValues(coconut);
        }

        _dbcontext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteCoconut")]
    public IActionResult Delete(int id)
    {
        var coconut = _dbcontext.Coconuts.Find(id);

        if (coconut is null)
            return NotFound();

        _dbcontext.Coconuts.Remove(coconut);
        _dbcontext.SaveChanges();
        return NoContent();
    }
}

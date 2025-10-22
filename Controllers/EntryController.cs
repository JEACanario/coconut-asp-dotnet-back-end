using System.Security.Claims;
using coconut_asp_dotnet_back_end.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

[ApiController]
[Route("[controller]")]
public class EntryController : ControllerBase
{
    private readonly ILogger<EntryController> _logger;
    private readonly CoconutContext _dbcontext;

    public EntryController(ILogger<EntryController> logger, CoconutContext dbcontext)
    {
        _dbcontext = dbcontext;
        _logger = logger;
    }

    [HttpGet(Name = "GetEntries")]
    public IEnumerable<Entry> Get()
    {
        var coconuts = _dbcontext.Entries;
        return coconuts;
    }

    [HttpPost(Name = "CreateEntry")]
    public IActionResult Create(Entry entry)
    {
        var entries = _dbcontext.Entries;
        var coconut = _dbcontext.Coconuts.Find(entry.CoconutId);

        if (coconut == null)
            return BadRequest("Invalid CoconutId");

        entry.Coconut = coconut;
        entries.Add(entry);
        _dbcontext.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
    }

    // We don't need to specify the coconut_id in the route, as it is already part of the Entry model
    [HttpPut("/entry/{id}", Name = "UpdateEntry")]
    public IActionResult Update(int coconut_id, int id, Entry entry)
    {
        if (id != entry.Id)
            return BadRequest();

        if (coconut_id != entry.CoconutId)
            return BadRequest();

        var user_id = User.Identity?.Name;
        var what = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        // Log the user ID and claim values for debugging - remove in production
        Console.WriteLine($"User ID: {user_id} ::: What: {what}");

        var entries = _dbcontext.Entries;

        var existingEntry = entries.Find(id);
        if (existingEntry is null)
            return NotFound();

        if (entries.Entry(existingEntry) is null)
        {
            entries.Update(existingEntry);
        }
        else
        {
            entries.Entry(existingEntry).CurrentValues.SetValues(entry);
        }

        _dbcontext.SaveChanges();

        return NoContent();
    }

    [HttpGet("/coconut/{id}/entry", Name = "GetCoconutEntries")]
    public IEnumerable<Entry> GetCoconutEntries(int id)
    {
        var coconut = _dbcontext.Coconuts.Find(id);

        if (coconut is null)
            return Enumerable.Empty<Entry>();

        var entries = _dbcontext.Entries.Where(e => e.CoconutId == id).ToList();
        return entries;
    }
}

using coconut_asp_dotnet_back_end.Models;
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
        entries.Add(entry);
        _dbcontext.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
    }
}

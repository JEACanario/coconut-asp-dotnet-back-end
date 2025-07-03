using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

using Models;

[ApiController]
[Route("[controller]")]
public class CoconutController : ControllerBase
{
    private readonly ILogger<CoconutController> _logger;
    private readonly CoconutContext _dbcontext;

    public CoconutController(ILogger<CoconutController> logger, CoconutContext coconutContext)
    {
        _logger = logger;
        _dbcontext = coconutContext;
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
    public IEnumerable<Coconut> Get()
    {
        var coconuts = _dbcontext.Coconuts;
        return coconuts;
    }

    [HttpPost(Name = "CreateCoconut")]
    public IActionResult Create(Coconut coconut)
    {
        var coconuts = _dbcontext.Coconuts;
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

    [HttpGet("{id}/entry", Name = "GetCoconutEntries")]
    public IEnumerable<Entry> GetCoconutEntries(int id)
    {
        var coconut = _dbcontext.Coconuts.Find(id);

        if (coconut is null)
            return Enumerable.Empty<Entry>();

        var entries = _dbcontext.Entries.Where(e => e.CoconutId == id).ToList();
        return entries;
    }
}

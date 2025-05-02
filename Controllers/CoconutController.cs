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

    [HttpGet(Name = "GetCoconut")]
    public IEnumerable<Coconut> Get()
    {
        var coconuts = _dbcontext.Coconuts;
        return coconuts; /* Enumerable
            .Range(1, 5)
            .Select(index => new Coconut
            {
                Id = index,
                UserId = index + 10,
                Status = (CoconutStatus)Random.Shared.Next(Enum.GetNames<CoconutStatus>().Length),
                Isbn = Random.Shared.Next(0, 1000000).ToString(),
                CoverUrl = "url",
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(0 - index)),
                EndDate = DateOnly.FromDateTime(DateTime.MinValue.AddDays(index)),
            })
            .ToArray(); ;*/
    }

    [HttpPost]
    public IActionResult Create(Coconut coconut)
    {
        var coconuts = _dbcontext.Coconuts;
        coconuts.Add(coconut);
        _dbcontext.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = coconut.Id }, coconut);
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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

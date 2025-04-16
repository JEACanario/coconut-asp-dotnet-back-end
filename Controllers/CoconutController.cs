using Microsoft.AspNetCore.Mvc;

namespace coconut_asp_dotnet_back_end.Controllers;

using Models;

[ApiController]
[Route("[controller]")]
public class CoconutController : ControllerBase
{
    private readonly ILogger<CoconutController> _logger;

    public CoconutController(ILogger<CoconutController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCoconut")]
    public IEnumerable<Coconut> Get()
    {
        return Enumerable
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
            .ToArray();
    }
}

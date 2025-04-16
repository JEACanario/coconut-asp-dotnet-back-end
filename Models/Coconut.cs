namespace coconut_asp_dotnet_back_end.Models;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class Coconut
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public CoconutStatus Status { get; set; }
    public string Isbn { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public void Bull()
    {
        var joao = new Coconut
        {
            UserId = 5,
            Status = (CoconutStatus)2,
            Isbn = "XIOIT",
            CoverUrl = "http",
            StartDate = DateOnly.FromDateTime(DateTime.Today),
            EndDate = DateOnly.MinValue,
        };
    }
}

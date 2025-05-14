namespace coconut_asp_dotnet_back_end.Models;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class Coconut
{
    public int Id { get; set; }
    public CoconutStatus Status { get; set; }
    public string Isbn { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    //belongs to one user
    public string UserId { get; set; }
    public AppUser? User { get; set; }

    // has many entries
    public List<Entry> Entries { get; set; } = new List<Entry>();
}

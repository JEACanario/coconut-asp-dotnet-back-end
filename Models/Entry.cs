using System.Text.Json.Serialization;

namespace coconut_asp_dotnet_back_end.Models;

public class Entry
{
    public int Id { set; get; }

    public string Title { set; get; }

    public DateOnly CreationDate { set; get; }

    public string Content { set; get; }

    public int CoconutId { get; set; }
    [JsonIgnore]
    public Coconut Coconut { get; set; } = null!;
}

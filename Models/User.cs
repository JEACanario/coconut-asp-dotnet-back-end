namespace coconut_asp_dotnet_back_end.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Name { get; set; }
}

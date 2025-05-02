namespace coconut_asp_dotnet_back_end.Data;

using coconut_asp_dotnet_back_end.Models;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Initialize(CoconutContext context)
    {
        // Look for any students.
        if (context.Coconuts.Any())
        {
            return; // DB has been seeded
        }

        var users = new User[]
        {
            new User
            {
                Email = "Carson@mail.com",
                Password = "Carson",
                Name = "Carson",
            },
            new User
            {
                Email = "Meredith@mail.com",
                Password = "Meredith",
                Name = "Meredith",
            },
            new User
            {
                Email = "Arturo@mail.com",
                Password = "Arturo",
                Name = "Arturo",
            },
            new User
            {
                Email = "Gytis@mail.com",
                Password = "Gytis",
                Name = "Gytis",
            },
            new User
            {
                Email = "Yan@mail.com",
                Password = "Yan",
                Name = "Yan",
            },
            new User
            {
                Email = "Peggy@mail.com",
                Password = "Peggy",
                Name = "Peggy",
            },
            new User
            {
                Email = "Laura@mail.com",
                Password = "Laura",
                Name = "Laura",
            },
            new User
            {
                Email = "Nino@mail.com",
                Password = "Nino",
                Name = "Nino",
            },
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        Console.WriteLine(context.Users.Where(u => u.Id == 1).First());

        var coconuts = new Coconut[]
        {
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Alexander",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 1,
                User = context.Users.Where(u => u.Id == 1).First(),
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Alonso",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 2,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Anand",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 3,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Barzdukas",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 4,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Li",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 1,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Justice",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 2,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Norman",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 3,
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Olivetto",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = 4,
            },
        };

        context.Coconuts.AddRange(coconuts);
        context.SaveChanges();

        var entries = new Entry[]
        {
            new Entry
            {
                CoconutId = 1,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 2,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 3,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 1,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 2,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 3,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 1,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 2,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 1,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 2,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 6,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
            new Entry
            {
                CoconutId = 3,
                Title = "We dont carrrre",
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Content = "None",
            },
        };

        context.Entries.AddRange(entries);
        context.SaveChanges();
        Console.WriteLine(context.Users.Where(u => u.Id == 1).First().Name);
    }
}

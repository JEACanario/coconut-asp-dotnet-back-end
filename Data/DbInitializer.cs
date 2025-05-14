namespace coconut_asp_dotnet_back_end.Data;

using coconut_asp_dotnet_back_end.Models;
using Microsoft.AspNetCore.Identity;
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

        var users = new AppUser[]
        {
            new AppUser
            {
                Email = "Carson@mail.com",

                Name = "Carson",
            },
            new AppUser
            {
                Email = "Meredith@mail.com",

                Name = "Meredith",
            },
            new AppUser
            {
                Email = "Arturo@mail.com",

                Name = "Arturo",
            },
            new AppUser
            {
                Email = "Gytis@mail.com",

                Name = "Gytis",
            },
            new AppUser
            {
                Email = "Yan@mail.com",

                Name = "Yan",
            },
            new AppUser
            {
                Email = "Peggy@mail.com",

                Name = "Peggy",
            },
            new AppUser
            {
                Email = "Laura@mail.com",

                Name = "Laura",
            },
            new AppUser
            {
                Email = "Nino@mail.com",

                Name = "Nino",
            },
        };



        foreach (var user in users)
        {
            var password = new PasswordHasher<AppUser>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;
        }

        context.Users.AddRange(users);
        context.SaveChanges();
        var db_users = context.Users.ToArray();
        var coconuts = new Coconut[]
        {
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Alexander",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[0].Id,
                User = users[0],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Alonso",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[1].Id,
                User = users[1],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Anand",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[2].Id,
                User = users[2],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Barzdukas",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[3].Id,
                User = users[3],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Li",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[0].Id,
                User = users[0],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Justice",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[1].Id,
                User = users[1],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Norman",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[2].Id,
                User = users[2],
            },
            new Coconut
            {
                Status = CoconutStatus.New,
                Isbn = "Olivetto",
                CoverUrl = "",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                UserId = users[3].Id,
                User = users[3],
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
        Console.WriteLine(context.Users.First().Name);
    }
}

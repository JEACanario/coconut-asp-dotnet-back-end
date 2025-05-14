namespace coconut_asp_dotnet_back_end.Models;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class CoconutContext(DbContextOptions<CoconutContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Coconut> Coconuts { get; set; }
    public DbSet<Entry> Entries { get; set; }

    public new DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coconut>().Property(b => b.Status).IsRequired();

        modelBuilder.Entity<Coconut>().ToTable("Coconut");
        modelBuilder.Entity<Entry>().ToTable("Entry");
        modelBuilder.Entity<AppUser>().ToTable("AppUser");
        base.OnModelCreating(modelBuilder);
    }

    // DbContextOptions<TContext> object in its constructor and passes it to the base constructor for DbContext
}

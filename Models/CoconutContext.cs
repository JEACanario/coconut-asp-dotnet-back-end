namespace coconut_asp_dotnet_back_end.Models;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class CoconutContext(DbContextOptions<CoconutContext> options) : DbContext(options)
{
    public DbSet<Coconut> Coconuts { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coconut>().Property(b => b.Status).IsRequired();
    }
}

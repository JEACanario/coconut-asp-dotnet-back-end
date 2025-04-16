public class CoconutContext : DbContext
{
    public DbSet<Coconut> Coconuts { get; set; }
    public DbSet<Entry> Entries { get; set; }
}

using Microsoft.EntityFrameworkCore;

namespace PrimitiveCollectionInJsonInitProblem;

public class Pub
{
    public Pub(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Visits Visits { get; set; } = null!;
}

public class Visits
{
    public string? LocationTag { get; set; }
    public List<DateOnly> DaysVisited { get; init; } = null!; // Change this from `init` to `set` and it works
}

internal class TestContext : DbContext
{
    public DbSet<Pub> Pubs => Set<Pub>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost,5433;Initial Catalog=PrimitiveCollectionsProblem;User Id=sa;Password=Pass@word;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pub>(
            b =>
            {
                b.OwnsOne(e => e.Visits).ToJson();
            });
    }
}
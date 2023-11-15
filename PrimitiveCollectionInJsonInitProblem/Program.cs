using Microsoft.EntityFrameworkCore;
using PrimitiveCollectionInJsonInitProblem;

TestContext.UseSqlite = false;

await using (var ctx = new TestContext())
{
    await ctx.Database.EnsureDeletedAsync();
    await ctx.Database.EnsureCreatedAsync();
}

await using (var ctx = new TestContext())
{
    var user = new Pub("MyPub")
    {
        Visits = new Visits
        {
            LocationTag = "tag",
            DaysVisited = new List<DateOnly> { new(2023, 1, 1) }
        }
    };

    ctx.Pubs.Add(user);
    await ctx.SaveChangesAsync();
}

await using (var ctx = new TestContext())
{
    var pubs = await ctx.Pubs.ToListAsync();
    var pub1 = await ctx.Pubs
        .Where(u => u.Visits.DaysVisited.Contains(new DateOnly(2023, 1, 1)))
        .FirstOrDefaultAsync();
}
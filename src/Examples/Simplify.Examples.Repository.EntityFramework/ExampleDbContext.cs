using Microsoft.EntityFrameworkCore;

namespace Simplify.Examples.Repository.EntityFramework;

public class ExampleDbContext(DbContextOptions options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleDbContext).Assembly);
}
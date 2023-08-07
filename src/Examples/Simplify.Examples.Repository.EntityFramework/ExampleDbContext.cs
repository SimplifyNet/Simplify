using Microsoft.EntityFrameworkCore;

namespace Simplify.Examples.Repository.EntityFramework;

public class ExampleDbContext : DbContext
{
	public ExampleDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleDbContext).Assembly);
}
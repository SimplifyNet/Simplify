using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simplify.Examples.Repository.EntityFramework.Location;
using Simplify.Repository.EntityFramework.Mappings;

namespace Simplify.Examples.Repository.EntityFramework.Mappings;

public class CityMap : IdentityObjectMap<City>
{
	protected override void ConfigureEntity(EntityTypeBuilder<City> builder)
	{
		builder.ToTable("Cities");

		builder.HasMany(x => x.CityNames);
	}
}
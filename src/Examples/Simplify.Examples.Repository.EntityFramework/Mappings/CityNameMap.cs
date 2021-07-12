using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simplify.Examples.Repository.EntityFramework.Location;
using Simplify.Repository.EntityFramework.Mappings;

namespace Simplify.Examples.Repository.EntityFramework.Mappings
{
	public class CityNameMap : NamedObjectMap<CityName>
	{
		protected override void ConfigureEntity(EntityTypeBuilder<CityName> builder)
		{
			builder.ToTable("CitiesNames");

			builder.HasOne(x => x.City)
				.WithMany();

			builder.Property(x => x.Language);

			base.ConfigureEntity(builder);
		}
	}
}
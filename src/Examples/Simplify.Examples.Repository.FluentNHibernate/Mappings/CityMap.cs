using Simplify.Examples.Repository.FluentNHibernate.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.Examples.Repository.FluentNHibernate.Mappings;

public class CityMap : IdentityObjectMap<City>
{
	public CityMap()
	{
		Table("Cities");

		HasMany<CityName>(x => x.CityNames)
			.Inverse()
			.Cascade.All()
			.Cascade.AllDeleteOrphan();
	}
}
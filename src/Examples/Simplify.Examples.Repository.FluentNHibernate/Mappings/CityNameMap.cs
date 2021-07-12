using Simplify.Examples.Repository.FluentNHibernate.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.Examples.Repository.FluentNHibernate.Mappings
{
	public class CityNameMap : NamedObjectMap<CityName>
	{
		public CityNameMap()
		{
			Table("CitiesNames");

			References<City>(x => x.City);
			Map(x => x.Language);
		}
	}
}
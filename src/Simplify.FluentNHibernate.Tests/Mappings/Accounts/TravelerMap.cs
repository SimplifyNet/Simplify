using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class TravelerMap : ClassMap<Traveler>
	{
		public TravelerMap()
		{
			Table("Travelers");

			Id(x => x.ID);

			Map(x => x.Name);

			References(x => x.Employee).Unique();
		}
	}
}
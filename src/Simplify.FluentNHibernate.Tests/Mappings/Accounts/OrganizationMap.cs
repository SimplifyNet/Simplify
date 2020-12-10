using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class OrganizationMap : ClassMap<Organization>
	{
		public OrganizationMap()
		{
			Table("Organizations");

			Id(x => x.ID);

			Map(x => x.Name);

			HasMany(x => x.Users);
		}
	}
}
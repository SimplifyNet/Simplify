using Simplify.Examples.Repository.FluentNHibernate.Accounts;
using Simplify.Examples.Repository.FluentNHibernate.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.Examples.Repository.FluentNHibernate.Mappings;

public class UserMap : NamedObjectMap<User>
{
	public UserMap()
	{
		Map(x => x.Password);

		Map(x => x.EMail);

		References<City>(x => x.City);

		Map(x => x.LastActivityTime);
	}
}
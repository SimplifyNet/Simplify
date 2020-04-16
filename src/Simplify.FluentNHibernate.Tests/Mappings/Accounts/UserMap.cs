using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Table("Users");

			Id(x => x.ID);

			Map(x => x.Name);

			Map(x => x.Password);
			Map(x => x.EMail);
			Map(x => x.LastActivityTime);

			HasOne(x => x.Employee).Cascade.All();

			HasManyToMany(x => x.Groups)
				.Table("UsersGroups");

			HasMany(x => x.Privileges)
				.KeyColumn("UserID")
				.ForeignKeyConstraintName("FK_UsersPrivileges_UserID")
				.Table("UsersPrivileges")
				.Element("Type");

			References(x => x.Organization);
		}
	}
}
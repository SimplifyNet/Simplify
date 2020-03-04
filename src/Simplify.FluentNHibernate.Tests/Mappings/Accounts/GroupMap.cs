using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class GroupMap : ClassMap<Group>
	{
		public GroupMap()
		{
			Table("Groups");

			Id(x => x.ID);

			Map(x => x.Name);

			HasManyToMany(x => x.Users)
				.Table("UsersGroups");

			HasMany(x => x.Privileges)
				.KeyColumn("GroupID")
				.ForeignKeyConstraintName("FK_Custom_UsersPrivileges_GroupID")
				.Table("GroupsPrivileges")
				.Element("Type");
		}
	}
}
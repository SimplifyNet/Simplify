using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class EmployeeMap : ClassMap<Employee>
	{
		public EmployeeMap()
		{
			Table("Employees");

			Id(x => x.ID);

			Map(x => x.Name);

			HasOne(x => x.User).Constrained();

			HasOne(x => x.Traveler).PropertyRef(x => x.Employee);
		}
	}
}
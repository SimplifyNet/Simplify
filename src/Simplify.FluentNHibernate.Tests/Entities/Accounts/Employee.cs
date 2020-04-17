namespace Simplify.FluentNHibernate.Tests.Entities.Accounts
{
	public class Employee
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual User User { get; set; }

		public virtual Traveler Traveler { get; set; }
	}
}
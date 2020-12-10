namespace Simplify.FluentNHibernate.Tests.Entities.Accounts
{
	public class Traveler
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual Employee Employee { get; set; }
	}
}
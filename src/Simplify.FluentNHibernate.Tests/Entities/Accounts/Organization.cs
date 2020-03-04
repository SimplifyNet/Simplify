using System.Collections.Generic;

namespace Simplify.FluentNHibernate.Tests.Entities.Accounts
{
	public class Organization
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual IList<User> Users { get; set; }
	}
}
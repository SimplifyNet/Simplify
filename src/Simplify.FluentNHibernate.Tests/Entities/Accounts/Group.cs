using System.Collections.Generic;

namespace Simplify.FluentNHibernate.Tests.Entities.Accounts
{
	public class Group
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual IList<Privilege> Privileges { get; set; } = new List<Privilege>();

		public virtual IList<User> Users { get; set; }
	}
}
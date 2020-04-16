using System;
using System.Collections.Generic;

namespace Simplify.FluentNHibernate.Tests.Entities.Accounts
{
	public class User
	{
		public virtual int ID { get; set; }

		public virtual string Name { get; set; }

		public virtual string Password { get; set; }

		public virtual string EMail { get; set; }

		public virtual DateTime LastActivityTime { get; set; }

		public virtual IList<Privilege> Privileges { get; set; } = new List<Privilege>();

		public virtual IList<Group> Groups { get; set; }

		public virtual Organization Organization { get; set; }
		public virtual Employee Employee { get; set; }
	}
}
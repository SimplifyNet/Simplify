using System;
using Simplify.Examples.Repository.Domain.Accounts;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.Examples.Repository.FluentNHibernate.Accounts;

public class User : NamedObject, IUser
{
	public virtual string Password { get; set; }

	public virtual string EMail { get; set; }

	public virtual ICity City { get; set; }

	public virtual DateTime LastActivityTime { get; set; }
}
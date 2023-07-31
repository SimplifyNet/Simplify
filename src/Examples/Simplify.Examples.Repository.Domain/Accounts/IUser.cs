using System;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository;

namespace Simplify.Examples.Repository.Domain.Accounts;

public interface IUser : INamedObject
{
	string Password { get; set; }

	string EMail { get; set; }

	ICity City { get; set; }

	DateTime LastActivityTime { get; set; }
}
using Simplify.Bus.Impl.Tests.Domain.Users;

namespace Simplify.Bus.Impl.Tests.Database.Users;

public class User : IUser
{
	public string? Name { get; set; }
}
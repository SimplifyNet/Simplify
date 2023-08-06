using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Database.Users;

public class User : IUser
{
	public int ID { get; set; }
	public string? Name { get; set; }
}
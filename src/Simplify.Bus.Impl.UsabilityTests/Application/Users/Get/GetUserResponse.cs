using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;

public class GetUserResponse
{
	public GetUserResponse(IUser? user) => User = user;

	public IUser? User { get; }
}
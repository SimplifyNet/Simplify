using System;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create;

public class CreateUserCommand
{
	public CreateUserCommand(IUser user) => User = user ?? throw new ArgumentNullException(nameof(user));

	public IUser User { get; }
}
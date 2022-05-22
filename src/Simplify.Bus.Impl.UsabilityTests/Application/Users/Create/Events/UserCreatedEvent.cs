using System;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Events;

public class UserCreatedEvent
{
	public UserCreatedEvent(IUser user) => User = user ?? throw new ArgumentNullException(nameof(user));

	public IUser User { get; }
}
using System;
using Simplify.Bus.Impl.Tests.Domain.Users;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class UserCreatedEvent : IEvent
{
	public UserCreatedEvent(IUser user) => User = user ?? throw new ArgumentNullException(nameof(user));

	public IUser User { get; }
}
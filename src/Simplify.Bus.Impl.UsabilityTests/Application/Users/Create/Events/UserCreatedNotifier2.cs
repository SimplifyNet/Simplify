using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Events;

public class UserCreatedNotifier2 : IEventHandler<UserCreatedEvent>
{
	public Task Handle(UserCreatedEvent @event)
	{
		Console.WriteLine($"{nameof(UserCreatedNotifier2)} executed");

		return Task.CompletedTask;
	}
}
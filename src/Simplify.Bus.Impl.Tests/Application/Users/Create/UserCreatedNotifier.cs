using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create
{
	public class UserCreatedNotifier : IEventHandler<UserCreatedEvent>
	{
		public bool Executed { get; private set; }

		public Task Handle(UserCreatedEvent @event)
		{
			Executed = true;

			Console.WriteLine($"{nameof(UserCreatedNotifier)} executed");

			return Task.CompletedTask;
		}
	}
}
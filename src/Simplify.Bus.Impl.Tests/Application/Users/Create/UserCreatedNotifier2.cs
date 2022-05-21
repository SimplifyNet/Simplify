using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create
{
	public class UserCreatedNotifier2 : IEventHandler<UserCreatedEvent>
	{
		public Task Handle(UserCreatedEvent @event)
		{
			ActionsAuditor.ExecutedActions.Add(typeof(UserCreatedNotifier2));

			Console.WriteLine($"{nameof(UserCreatedNotifier2)} executed");

			return Task.CompletedTask;
		}
	}
}
using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create
{
	public class UserCreatedNotifier : IEventHandler<UserCreatedEvent>
	{
		public Task Handle(UserCreatedEvent @event)
		{
			ActionsAuditor.ExecutedActions.Add(typeof(UserCreatedNotifier));

			Console.WriteLine($"{nameof(UserCreatedNotifier)} executed");

			return Task.CompletedTask;
		}
	}
}
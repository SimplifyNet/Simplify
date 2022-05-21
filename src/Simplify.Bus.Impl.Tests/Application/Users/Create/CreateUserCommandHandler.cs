using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
	public Task Handle(CreateUserCommand command)
	{
		ActionsAuditor.ExecutedActions.Add(typeof(CreateUserCommandHandler));

		Console.WriteLine($"{nameof(CreateUserCommandHandler)} executed");
		Console.WriteLine($"{command.User.Name} user handled");

		return Task.CompletedTask;
	}
}
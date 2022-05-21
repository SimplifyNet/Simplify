using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserCommandValidationBehavior : IBehavior<CreateUserCommand>
{
	public Task Handle(CreateUserCommand command)
	{
		ActionsAuditor.ExecutedActions.Add(typeof(CreateUserCommandValidationBehavior));

		Console.WriteLine($"{nameof(CreateUserCommandValidationBehavior)} executed");

		return Task.CompletedTask;
	}
}
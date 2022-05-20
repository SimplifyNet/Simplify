using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserCommandValidationBehavior : IBehavior<CreateUserCommand>
{
	public bool Executed { get; private set; }

	public Task Handle(CreateUserCommand command)
	{
		Console.WriteLine($"{nameof(CreateUserCommandValidationBehavior)} executed");

		return Task.CompletedTask;
	}
}
using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
	public bool Executed { get; private set; }

	public Task Handle(CreateUserCommand command)
	{
		Executed = true;

		Console.WriteLine($"{nameof(CreateUserCommandHandler)} executed");

		return Task.CompletedTask;
	}
}
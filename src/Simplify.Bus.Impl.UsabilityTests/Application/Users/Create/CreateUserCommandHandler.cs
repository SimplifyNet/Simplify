using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
	public Task Handle(CreateUserCommand command)
	{
		Console.WriteLine($"{nameof(CreateUserCommandHandler)} executed");
		Console.WriteLine($"{command.User.Name} user handled");

		return Task.CompletedTask;
	}
}
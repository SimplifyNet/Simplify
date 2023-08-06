using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Validation;

public class CreateUserCommandValidationBehavior : IBehavior<CreateUserCommand>
{
	public Task Handle(CreateUserCommand command, RequestHandler next)
	{
		Console.WriteLine($"{nameof(CreateUserCommandValidationBehavior)} executed");

		return next();
	}
}
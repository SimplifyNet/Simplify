using FluentValidation;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Validation;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(x => x.User)
			.SetValidator(new CreateUserValidator());
	}
}
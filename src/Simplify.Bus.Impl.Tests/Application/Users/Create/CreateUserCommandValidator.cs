using FluentValidation;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(x => x.User)
			.SetValidator(new CreateUserValidator());
	}
}
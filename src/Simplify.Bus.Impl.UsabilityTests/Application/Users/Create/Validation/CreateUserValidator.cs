using FluentValidation;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Validation;

public class CreateUserValidator : AbstractValidator<IUser>
{
	public CreateUserValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty();
	}
}
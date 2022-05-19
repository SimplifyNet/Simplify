using FluentValidation;
using Simplify.Bus.Impl.Tests.Domain.Users;

namespace Simplify.Bus.Impl.Tests.Application.Users.Create;

public class CreateUserValidator : AbstractValidator<IUser>
{
	public CreateUserValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty();
	}
}
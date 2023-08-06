using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;

public class GetUserQueryValidationBehavior : IBehavior<GetUserQuery, GetUserResponse>
{
	public Task<GetUserResponse> Handle(GetUserQuery command, RequestHandler<GetUserResponse> next)
	{
		Console.WriteLine($"{nameof(GetUserQueryValidationBehavior)} executed");

		return next();
	}
}
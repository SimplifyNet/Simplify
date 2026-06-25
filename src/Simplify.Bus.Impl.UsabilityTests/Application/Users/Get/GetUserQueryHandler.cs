using System.Threading.Tasks;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;

public class GetUserQueryHandler(IUsersRepository repository) : IRequestHandler<GetUserQuery, GetUserResponse>
{
	public async Task<GetUserResponse> Handle(GetUserQuery request) => new GetUserResponse(await repository.GetAsync(request.UserID));
}
using System.Threading.Tasks;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
	private readonly IUsersRepository _repository;

	public GetUserQueryHandler(IUsersRepository repository) => _repository = repository;

	public async Task<GetUserResponse> Handle(GetUserQuery request) => new GetUserResponse(await _repository.GetAsync(request.UserID));
}
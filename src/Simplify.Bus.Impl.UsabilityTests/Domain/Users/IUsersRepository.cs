using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Domain.Users;

public interface IUsersRepository
{
	Task CreateAsync(IUser user);

	Task<IUser?> GetAsync(int id);
}
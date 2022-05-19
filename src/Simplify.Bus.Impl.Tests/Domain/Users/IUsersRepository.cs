using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Domain.Users;

public interface IUsersRepository
{
	Task Create(IUser user);
}
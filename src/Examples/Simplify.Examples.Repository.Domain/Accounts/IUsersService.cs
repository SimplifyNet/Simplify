using System.Threading.Tasks;
using Simplify.Examples.Repository.Domain.Location;

namespace Simplify.Examples.Repository.Domain.Accounts
{
	public interface IUsersService
	{
		Task<int> CreateUserAsync(IUser user);

		IUser GetUser(string userName);

		void SetUserCity(IUser user, ICity city);
	}
}
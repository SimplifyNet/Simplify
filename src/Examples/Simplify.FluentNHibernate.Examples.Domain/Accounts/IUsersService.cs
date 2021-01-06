using System.Threading.Tasks;
using Simplify.FluentNHibernate.Examples.Domain.Location;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public interface IUsersService
	{
		Task<int> CreateUserAsync(IUser user);

		IUser GetUser(string userName);

		void SetUserCity(IUser user, ICity city);
	}
}
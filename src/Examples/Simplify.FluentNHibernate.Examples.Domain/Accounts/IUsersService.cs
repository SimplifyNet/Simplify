using Simplify.FluentNHibernate.Examples.Domain.Location;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public interface IUsersService
	{
		IUser GetUser(string userName);

		void SetUserCity(IUser user, ICity city);
	}
}
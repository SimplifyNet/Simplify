using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplify.Bus.Impl.UsabilityTests.Domain.Users;

namespace Simplify.Bus.Impl.UsabilityTests.Database.Users;

public class UsersRepository : IUsersRepository
{
	public IList<IUser> DataStore { get; } = new List<IUser>();

	public Task CreateAsync(IUser user)
	{
		DataStore.Add(user);

		return Task.CompletedTask;
	}

	public Task<IUser?> GetAsync(int id) => Task.FromResult(DataStore.FirstOrDefault(x => x.ID == id));
}
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.Application.Users.Create;
using Simplify.Bus.Impl.Tests.Database.Users;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class BusAsyncTests
{
	[Test]
	public async Task CreateUserCommand_HandlerWithEvent_Executed()
	{
		// Arrange

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		var bus = new BusAsync();

		// Act
		await bus.Send(command);

		// Assert
	}
}
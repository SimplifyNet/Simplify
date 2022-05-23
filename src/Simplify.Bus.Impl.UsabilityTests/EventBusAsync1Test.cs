using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Events;
using Simplify.Bus.Impl.UsabilityTests.Database.Users;
using Simplify.DI;

namespace Simplify.Bus.Impl.UsabilityTests;

[TestFixture]
public class EventBusAsync1Test : DIContainerTestFixtureBase
{
	[Test]
	public async Task Publish_TwoEvents_NoExceptions()
	{
		// Arrange

		Container
			.Register<UserCreatedNotifier>()
			.Register<UserCreatedNotifier2>()

			.RegisterEventBus<UserCreatedEvent>(
				typeof(UserCreatedNotifier),
				typeof(UserCreatedNotifier2));

		var busEvent = new UserCreatedEvent(new User
		{
			Name = "Test User"
		});

		// Act

		using var scope = Container.BeginLifetimeScope();
		var bus = scope.Resolver.Resolve<IEventBusAsync<UserCreatedEvent>>();

		await bus.Publish(busEvent);
	}
}
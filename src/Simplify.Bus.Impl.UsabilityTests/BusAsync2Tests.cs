using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;
using Simplify.DI;

namespace Simplify.Bus.Impl.UsabilityTests;

[TestFixture]
public class Bus2Async2Tests : DIContainerTestFixtureBase
{
	[Test]
	public async Task Send_CommandWithoutBehaviors_NoExceptions()
	{
		// Arrange

		Container
			.Register<IRequestHandler<GetUserQuery, GetUserResponse>, GetUserQueryHandler>()
			.RegisterBus<GetUserQuery>();

		var query = new GetUserQuery(1);

		// Act

		using var scope = Container.BeginLifetimeScope();

		await scope.Resolver.Resolve<IBusAsync<GetUserQuery>>()
			.Send(query);
	}

	[Test]
	public async Task Send_CommandWithBehaviors_NoExceptions()
	{
		// Arrange

		Container
			.Register<IRequestHandler<GetUserQuery, GetUserResponse>, GetUserQueryHandler>()
			.RegisterBus<GetUserQuery>(
				typeof(GetUserQueryValidationBehavior)
			);

		var query = new GetUserQuery(1);

		// Act

		using var scope = Container.BeginLifetimeScope();

		await scope.Resolver.Resolve<IBusAsync<GetUserQuery>>()
			.Send(query);
	}
}
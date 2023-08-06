using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.UsabilityTests.Application.Users.Create;
using Simplify.Bus.Impl.UsabilityTests.Application.Users.Create.Validation;
using Simplify.Bus.Impl.UsabilityTests.Database.Users;
using Simplify.Bus.Impl.UsabilityTests.Infrastructure.Log;
using Simplify.DI;

namespace Simplify.Bus.Impl.UsabilityTests;

[TestFixture]
public class BusAsync1Tests : DIContainerTestFixtureBase
{
	[Test]
	public async Task Send_CommandWithoutBehaviors_NoExceptions()
	{
		// Arrange

		Container
			.Register<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>()
			.RegisterBus<CreateUserCommand>();

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		// Act

		using var scope = Container.BeginLifetimeScope();

		await scope.Resolver.Resolve<IBusAsync<CreateUserCommand>>()
			.Send(command);
	}

	[Test]
	public async Task Send_CommandWithBehaviors_NoExceptions()
	{
		// Arrange

		Container
			.Register<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>()

			.Register<LoggingBehavior<CreateUserCommand>>()
			.Register<CreateUserCommandValidationBehavior>()

			.RegisterBus<CreateUserCommand>(
				typeof(LoggingBehavior<CreateUserCommand>),
				typeof(CreateUserCommandValidationBehavior)
			);

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		Container.Verify();

		// Act

		using var scope = Container.BeginLifetimeScope();

		await scope.Resolver.Resolve<IBusAsync<CreateUserCommand>>()
			.Send(command);
	}
}
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.Application.Users.Create;
using Simplify.Bus.Impl.Tests.Database.Users;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class BusAsyncTests
{
	[Test]
	public async Task CreateUserCommand_HandlerWithEvent_Executed()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container.RegisterBus<CreateUserCommand, CreateUserCommandHandler, UserCreatedEvent>();

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		// Act

		using var scope = container.BeginLifetimeScope();
		var bus = scope.Resolver.Resolve<IBusAsync<CreateUserCommand, UserCreatedEvent>>();

		await bus.Send(command);

		// Assert

		var handler = scope.Resolver.Resolve<CreateUserCommandHandler>();
		var notifier = scope.Resolver.Resolve<UserCreatedNotifier>();

		Assert.IsTrue(handler.Executed);
		Assert.IsTrue(notifier.Executed);
	}

	[Test]
	public async Task CreateUserCommand_HandlerWithEventAndCustomRegistrations_Executed()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container
			.Register<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>()
			.Register<UserCreatedNotifier>()

			.Register<IList<IEventHandler<UserCreatedEvent>>>(r => new List<IEventHandler<UserCreatedEvent>>
			{
				r.Resolve<UserCreatedNotifier>()
			})

			.Register<IBusAsync<CreateUserCommand, UserCreatedEvent>, BusAsync<CreateUserCommand, UserCreatedEvent>>();

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		// Act

		using var scope = container.BeginLifetimeScope();
		var bus = scope.Resolver.Resolve<IBusAsync<CreateUserCommand, UserCreatedEvent>>();

		await bus.Send(command);

		// Assert
	}
}
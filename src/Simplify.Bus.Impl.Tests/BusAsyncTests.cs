using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.Application.Users.Create;
using Simplify.Bus.Impl.Tests.Database.Users;
using Simplify.Bus.Impl.Tests.Infrastructure.Log;
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

		container
			.Register<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>()
			.Register<UserCreatedNotifier>()

			.RegisterBus<CreateUserCommand, UserCreatedEvent>(typeof(UserCreatedNotifier));

		var command = new CreateUserCommand(new User
		{
			Name = "Test User"
		});

		// Act

		using var scope = container.BeginLifetimeScope();
		var bus = scope.Resolver.Resolve<IBusAsync<CreateUserCommand, UserCreatedEvent>>();

		await bus.Send(command);

		// Assert

		Assert.AreEqual(5, ActionsAuditor.ExecutedActions.Count);
		Assert.AreEqual(typeof(LoggingBehavior), ActionsAuditor.ExecutedActions[0]);
		Assert.AreEqual(typeof(CreateUserCommandValidationBehavior), ActionsAuditor.ExecutedActions[1]);
		Assert.AreEqual(typeof(CreateUserCommandHandler), ActionsAuditor.ExecutedActions[2]);

		Assert.IsTrue(ActionsAuditor.ExecutedActions.Contains(typeof(UserCreatedNotifier)));
		Assert.Greater(ActionsAuditor.ExecutedActions.IndexOf(typeof(UserCreatedNotifier)), 2);

		Assert.IsTrue(ActionsAuditor.ExecutedActions.Contains(typeof(UserCreatedNotifier2)));
		Assert.Greater(ActionsAuditor.ExecutedActions.IndexOf(typeof(UserCreatedNotifier2)), 2);
	}

	[Test]
	public async Task CreateUserCommand_HandlerWithEvent_FullCustomRegistrations_Executed()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container
			.Register<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>()
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

		Assert.AreEqual(5, ActionsAuditor.ExecutedActions.Count);
		Assert.AreEqual(typeof(LoggingBehavior), ActionsAuditor.ExecutedActions[0]);
		Assert.AreEqual(typeof(CreateUserCommandValidationBehavior), ActionsAuditor.ExecutedActions[1]);
		Assert.AreEqual(typeof(CreateUserCommandHandler), ActionsAuditor.ExecutedActions[2]);

		Assert.IsTrue(ActionsAuditor.ExecutedActions.Contains(typeof(UserCreatedNotifier)));
		Assert.Greater(ActionsAuditor.ExecutedActions.IndexOf(typeof(UserCreatedNotifier)), 2);

		Assert.IsTrue(ActionsAuditor.ExecutedActions.Contains(typeof(UserCreatedNotifier2)));
		Assert.Greater(ActionsAuditor.ExecutedActions.IndexOf(typeof(UserCreatedNotifier2)), 2);
	}
}
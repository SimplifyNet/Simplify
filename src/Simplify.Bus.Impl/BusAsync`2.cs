using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync<T, TEvent> : IBusAsync<T, TEvent>
	where T : ICommand
	where TEvent : IEvent
{
	private readonly ICommandHandler<T> _commandHandler;
	private readonly IList<IEventHandler<TEvent>>? _eventHandlers;

	public BusAsync(ICommandHandler<T> commandHandler, IList<IEventHandler<TEvent>>? eventHandlers)
	{
		_commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
		_eventHandlers = eventHandlers;
	}

	public Task Send(T command)
	{
		return _commandHandler.Handle(command);
	}

	public Task Publish(TEvent @event)
	{
		throw new System.NotImplementedException();
	}
}
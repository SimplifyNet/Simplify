using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync<T, TEvent> : IBusAsync<T, TEvent>
	where T : IRequest
	where TEvent : IEvent
{
	private readonly IRequestHandler<T> _requestHandler;
	private readonly IReadOnlyCollection<IEventHandler<TEvent>>? _eventHandlers;

	public BusAsync(IRequestHandler<T> requestHandler, IReadOnlyCollection<IEventHandler<TEvent>>? eventHandlers)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_eventHandlers = eventHandlers;
	}

	public Task Send(T request)
	{
		return _requestHandler.Handle(request);
	}

	public Task Publish(TEvent @event)
	{
		throw new System.NotImplementedException();
	}
}
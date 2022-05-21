using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync<T, TResponse, TEvent> : IBusAsync<T, TResponse, TEvent>
	where T : IRequest
	where TResponse : IResponse
	where TEvent : IEvent
{
	private readonly IRequestHandler<T, TResponse> _requestHandler;
	private readonly IList<IEventHandler<TEvent>>? _eventHandlers;

	public BusAsync(IRequestHandler<T, TResponse> requestHandler, IList<IEventHandler<TEvent>>? eventHandlers)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_eventHandlers = eventHandlers;
	}

	public Task<TResponse> Send(T request)
	{
		return _requestHandler.Handle(request);
	}

	public Task Publish(TEvent @event)
	{
		throw new System.NotImplementedException();
	}
}
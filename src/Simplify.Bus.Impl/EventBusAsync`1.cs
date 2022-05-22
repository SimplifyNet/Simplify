using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class EventBusAsync<TEvent> : IEventBusAsync<TEvent>
{
	private readonly IReadOnlyCollection<IEventHandler<TEvent>>? _eventHandlers;

	public EventBusAsync(IReadOnlyCollection<IEventHandler<TEvent>>? eventHandlers = null) => _eventHandlers = eventHandlers;

	public Task Publish(TEvent @event)
	{
		throw new System.NotImplementedException();
	}
}
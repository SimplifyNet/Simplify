using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class EventBusAsync<TEvent> : IEventBusAsync<TEvent>
{
	private readonly IReadOnlyCollection<IEventHandler<TEvent>> _eventHandlers;
	private readonly PublishStrategy _publishStrategy;

	public EventBusAsync(IReadOnlyCollection<IEventHandler<TEvent>> eventHandlers, PublishStrategy publishStrategy = PublishStrategy.SyncStopOnException)
	{
		_eventHandlers = eventHandlers ?? throw new System.ArgumentNullException(nameof(eventHandlers));
		_publishStrategy = publishStrategy;
	}

	public async Task Publish(TEvent busEvent)
	{
		foreach (var item in _eventHandlers)
			await item.Handle(busEvent);
	}
}
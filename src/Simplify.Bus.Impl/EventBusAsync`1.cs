using System.Collections.Generic;
using System.Linq;
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
		if (_publishStrategy == PublishStrategy.SyncStopOnException)
			foreach (var item in _eventHandlers)
				await item.Handle(busEvent);
		else if (_publishStrategy == PublishStrategy.ParallelWhenAll)
			await Task.WhenAll(_eventHandlers.Select(x => x.Handle(busEvent)).ToArray());
	}
}
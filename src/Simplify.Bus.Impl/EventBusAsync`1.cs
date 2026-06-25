using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class EventBusAsync<TEvent>(IReadOnlyCollection<IEventHandler<TEvent>> eventHandlers, PublishStrategy publishStrategy = PublishStrategy.SyncStopOnException) : IEventBusAsync<TEvent>
{
	private readonly IReadOnlyCollection<IEventHandler<TEvent>> _eventHandlers = eventHandlers ?? throw new System.ArgumentNullException(nameof(eventHandlers));

	public async Task Publish(TEvent busEvent)
	{
		if (publishStrategy == PublishStrategy.SyncStopOnException)
			foreach (var item in _eventHandlers)
				await item.Handle(busEvent);
		else if (publishStrategy == PublishStrategy.ParallelWhenAll)
		{
			var whenAllTask = Task.WhenAll([.. _eventHandlers.Select(x => x.Handle(busEvent))]);

			try
			{
				await whenAllTask;
			}
			catch
			{
				// Awaiting a faulted Task.WhenAll rethrows only the first exception; when more than one
				// handler failed, surface all of them so no handler error is silently lost.
				if (whenAllTask.Exception is { InnerExceptions.Count: > 1 })
					throw whenAllTask.Exception;

				throw;
			}
		}
	}
}
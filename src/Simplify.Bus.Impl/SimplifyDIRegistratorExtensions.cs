using System;
using System.Collections.Generic;
using Simplify.DI;

namespace Simplify.Bus.Impl;

/// <summary>
/// Provides Simplify.Bus registrations
/// </summary>
public static class SimplifyDIRegistratorExtensions
{
	/// <summary>
	/// Registers Simplify.Bus.
	/// </summary>
	/// <param name="registrator">The registrator.</param>
	/// <returns></returns>
	public static IDIRegistrator RegisterBus<TRequest, TEvent>(this IDIRegistrator registrator, params Type[] eventHandlers)
		where TRequest : IRequest
		where TEvent : IEvent
	{
		registrator
			.Register<IBusAsync<TRequest, TEvent>, BusAsync<TRequest, TEvent>>();

		if (eventHandlers == null)
			return registrator;

		registrator.RegisterEventHandlersList<TEvent>(eventHandlers);

		return registrator;
	}

	private static IDIRegistrator RegisterEventHandlersList<TEvent>(this IDIRegistrator registrator, params Type[] eventHandlers)
		where TEvent : IEvent
		 => registrator.Register<IList<IEventHandler<TEvent>>>(r =>
			{
				var items = new List<IEventHandler<TEvent>>();

				foreach (var item in eventHandlers)
					items.Add((IEventHandler<TEvent>)r.Resolve(item));

				return items;
			});
}
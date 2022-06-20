using System;
using System.Collections.Generic;
using System.Linq;
using DryIoc;
using Simplify.DI;

namespace Simplify.Bus.Impl;

/// <summary>
/// Provides Simplify.Bus registrations
/// </summary>
public static class SimplifyDIRegistratorExtensions
{
	public static IDIRegistrator RegisterBus<TRequest>(this IDIRegistrator registrator, params Type[] behaviors) => registrator
			.Register<IBusAsync<TRequest>, BusAsync<TRequest>>()
			.RegisterBehaviorsList<TRequest>(behaviors.Where(x => x.ImplementsServiceType(typeof(IBehavior<TRequest>))).ToList());

	public static IDIRegistrator RegisterBus<TRequest, TResponse>(this IDIRegistrator registrator, params Type[] behaviors) => registrator
			.Register<IBusAsync<TRequest, TResponse>, BusAsync<TRequest, TResponse>>()
			.RegisterBehaviorsList<TRequest, TResponse>(behaviors.Where(x => x.ImplementsServiceType(typeof(IBehavior<TRequest, TResponse>))).ToList());

	public static IDIRegistrator RegisterEventBus<TEvent>(this IDIRegistrator registrator, params Type[] eventHandlers) => registrator
			.Register<IEventBusAsync<TEvent>, EventBusAsync<TEvent>>()
			.RegisterEventHandlersList<TEvent>(eventHandlers.Where(x => x.ImplementsServiceType(typeof(IEventHandler<TEvent>))).ToList());

	private static IDIRegistrator RegisterBehaviorsList<TRequest>(this IDIRegistrator registrator, ICollection<Type> behaviors)
		=> registrator.Register<IReadOnlyList<IBehavior<TRequest>>>(r =>
			behaviors.Select(item => (IBehavior<TRequest>)r.Resolve(item)).ToList());

	private static IDIRegistrator RegisterBehaviorsList<TRequest, TResponse>(this IDIRegistrator registrator, ICollection<Type> behaviors)
		=> registrator.Register<IReadOnlyList<IBehavior<TRequest, TResponse>>>(r =>
			behaviors.Select(item => (IBehavior<TRequest, TResponse>)r.Resolve(item)).ToList());

	private static IDIRegistrator RegisterEventHandlersList<TEvent>(this IDIRegistrator registrator, ICollection<Type> eventHandlers)
		=> registrator.Register<IReadOnlyList<IEventHandler<TEvent>>>(r =>
			eventHandlers.Select(item => (IEventHandler<TEvent>)r.Resolve(item)).ToList());
}
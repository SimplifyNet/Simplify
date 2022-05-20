namespace Simplify.Bus;

public interface IBusAsync<in T, TEvent> : ISenderAsync<T>, IPublisherAsync<TEvent>
	where T : ICommand
	where TEvent : IEvent
{
}
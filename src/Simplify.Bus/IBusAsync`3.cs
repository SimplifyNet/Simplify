namespace Simplify.Bus;

public interface IBusAsync<in T, TResponse, TEvent> : ISenderAsync<T, TResponse>, IPublisherAsync<TEvent>
	where T : ICommand
	where TResponse : IResponse
	where TEvent : IEvent
{
}
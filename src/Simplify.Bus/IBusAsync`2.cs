namespace Simplify.Bus;

public interface IBusAsync<in TRequest, TEvent> : ISenderAsync<TRequest>, IPublisherAsync<TEvent>
	where TRequest : IRequest
	where TEvent : IEvent
{
}
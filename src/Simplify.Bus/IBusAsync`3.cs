namespace Simplify.Bus;

public interface IBusAsync<in TRequest, TResponse, TEvent> : ISenderAsync<TRequest, TResponse>, IPublisherAsync<TEvent>
	where TRequest : IRequest
	where TResponse : IResponse
	where TEvent : IEvent
{
}
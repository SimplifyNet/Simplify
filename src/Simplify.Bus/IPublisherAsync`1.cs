using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IPublisherAsync<TEvent>
	where TEvent : IEvent
{
	Task Publish(TEvent @event);
}
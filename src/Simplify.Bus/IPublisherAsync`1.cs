using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IPublisherAsync<T>
	where T : IEvent
{
	Task Publish(T @event);
}
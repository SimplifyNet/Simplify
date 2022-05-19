using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IPublisherAsync
{
	Task Publish(IEvent @event);
}
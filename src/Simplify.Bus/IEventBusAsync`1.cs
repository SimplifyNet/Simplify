using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IEventBusAsync<in TEvent>
{
	Task Publish(TEvent busEvent);
}
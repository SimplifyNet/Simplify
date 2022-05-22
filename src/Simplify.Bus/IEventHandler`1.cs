using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IEventHandler<in TEvent>
{
	Task Handle(TEvent @event);
}
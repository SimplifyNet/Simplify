using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IEventHandler<in TEvent>
	where TEvent : IEvent
{
	Task Handle(TEvent @event);
}
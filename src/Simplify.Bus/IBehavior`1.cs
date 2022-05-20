using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<T>
	where T : ICommand
{
	Task Handle(T command);
}
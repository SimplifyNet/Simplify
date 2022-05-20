using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<T, TResponse>
	where T : ICommand
{
	Task<TResponse> Handle(T command);
}
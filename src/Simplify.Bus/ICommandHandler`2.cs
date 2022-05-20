using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ICommandHandler<in T, TResponse>
	where T : ICommand
{
	Task<TResponse> Handle(T command);
}
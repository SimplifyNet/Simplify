using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ICommandHandler<in T>
	where T : ICommand
{
	Task Handle(T command);
}
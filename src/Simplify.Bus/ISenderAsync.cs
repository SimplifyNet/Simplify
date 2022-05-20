using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ISenderAsync<in T>
	where T : ICommand
{
	Task Send(T command);
}
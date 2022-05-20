using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ISenderAsync<in T, TResponse>
	where T : ICommand
	where TResponse : IResponse
{
	Task<TResponse> Send(T command);
}
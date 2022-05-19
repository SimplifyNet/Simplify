using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ISenderAsync
{
	Task Send<T>(T command);

	Task<TResponse> Send<T, TResponse>(T command);
}
using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBusAsync<in TRequest, TResponse>
{
	Task<TResponse> Send(TRequest request);
}
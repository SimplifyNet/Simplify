using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ISenderAsync<in TRequest, TResponse>
	where TRequest : IRequest
	where TResponse : IResponse
{
	Task<TResponse> Send(TRequest request);
}
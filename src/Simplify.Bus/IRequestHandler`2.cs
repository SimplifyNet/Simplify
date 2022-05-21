using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IRequestHandler<in TRequest, TResponse>
	where TRequest : IRequest
{
	Task<TResponse> Handle(TRequest request);
}
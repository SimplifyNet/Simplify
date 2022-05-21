using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<TRequest, TResponse>
	where TRequest : IRequest
{
	Task<TResponse> Handle(TRequest request);
}
using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<in TRequest, TResponse>
{
	Task<TResponse> Handle(TRequest request, RequestHandler<TResponse> next);
}
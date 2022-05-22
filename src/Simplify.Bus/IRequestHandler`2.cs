using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IRequestHandler<in TRequest, TResponse>
{
	Task<TResponse> Handle(TRequest request);
}
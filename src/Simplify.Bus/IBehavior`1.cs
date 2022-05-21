using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<TRequest>
	where TRequest : IRequest
{
	Task Handle(TRequest request);
}
using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior<in TRequest>
{
	Task Handle(TRequest request, RequestHandler next);
}
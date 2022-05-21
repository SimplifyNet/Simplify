using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IRequestHandler<in TRequest>
	where TRequest : IRequest
{
	Task Handle(TRequest request);
}
using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IRequestHandler<in TRequest>
{
	Task Handle(TRequest request);
}
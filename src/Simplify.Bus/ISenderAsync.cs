using System.Threading.Tasks;

namespace Simplify.Bus;

public interface ISenderAsync<in TRequest>
	where TRequest : IRequest
{
	Task Send(TRequest request);
}
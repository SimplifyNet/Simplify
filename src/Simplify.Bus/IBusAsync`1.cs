using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBusAsync<in TRequest>
{
	Task Send(TRequest request);
}
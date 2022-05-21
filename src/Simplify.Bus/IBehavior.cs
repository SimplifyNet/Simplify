using System.Threading.Tasks;

namespace Simplify.Bus;

public interface IBehavior
{
	Task Handle(object command);
}
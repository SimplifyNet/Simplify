using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.UsabilityTests.Infrastructure.Log;

public class LoggingBehavior<TRequest> : IBehavior<TRequest>
{
	public Task Handle(TRequest request, RequestHandler next)
	{
		Console.WriteLine("LoggingBehavior executed");

		return next();
	}
}
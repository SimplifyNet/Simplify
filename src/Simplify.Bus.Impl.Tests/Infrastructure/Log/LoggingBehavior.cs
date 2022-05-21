using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl.Tests.Infrastructure.Log;

public class LoggingBehavior : IBehavior
{
	public Task Handle(object command)
	{
		ActionsAuditor.ExecutedActions.Add(typeof(LoggingBehavior));

		Console.WriteLine($"{nameof(LoggingBehavior)} executed");

		return Task.CompletedTask;
	}
}
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Simplify.Scheduler.IntegrationTester;

public class TwoParallelTasksProcessor
{
	public async Task Execute() => await DoWork();

	private Task DoWork()
	{
		Trace.WriteLine("--- TwoParallelTasksProcessor launched");

		Thread.Sleep(5000);

		return Task.CompletedTask;
	}
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simplify.Scheduler.IntegrationTester;

public class BasicTaskProcessorAsync(DisposableDependency dependency) : IDisposable
{
	private static bool _isRunning;

	public Task Run()
	{
		if (_isRunning)
			throw new SimplifySchedulerException("BasicTaskProcessor is running a duplicate!");

		_isRunning = true;

		Trace.WriteLine("BasicTaskProcessor launched");

		dependency.DoSomeWork();

		return Task.CompletedTask;
	}

	public void Dispose() => Trace.WriteLine("BasicTaskProcessor disposed");
}
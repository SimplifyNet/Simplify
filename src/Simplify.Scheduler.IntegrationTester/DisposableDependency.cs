using System;
using System.Diagnostics;

namespace Simplify.Scheduler.IntegrationTester;

public class DisposableDependency : IDisposable
{
	public void DoSomeWork() { }

	public void Dispose() => Trace.WriteLine("Disposable dependency disposed");
}
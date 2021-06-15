using System;

namespace Simplify.Scheduler.SimpleApp
{
	public class PeriodicalProcessor : IDisposable
	{
		public void Run() => Console.WriteLine("PeriodicalProcessor launched");

		public void Dispose() => Console.WriteLine("PeriodicalProcessor disposed");
	}
}
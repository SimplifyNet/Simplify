using System;
using Simplify.DI;
using Simplify.Scheduler.SimpleApp.Setup;

namespace Simplify.Scheduler.SimpleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// IOC container setup

			IocRegistrations.Register().Verify();

			// Using scheduler

			using (var scheduler = new MultitaskScheduler())
			{
				scheduler.OnJobStart += HandlerOnJobStart;
				scheduler.OnJobFinish += HandlerOnJobFinish;

				scheduler.AddJob<PeriodicalProcessor>(IocRegistrations.Configuration);

				if (scheduler.Start(args))
					return;
			}

			// Testing without scheduler
			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<PeriodicalProcessor>().Run();
		}

		private static void HandlerOnJobStart(Jobs.ISchedulerJobRepresentation representation)
		{
			Console.WriteLine("Job started: " + representation.JobClassType.Name);
		}

		private static void HandlerOnJobFinish(Jobs.ISchedulerJobRepresentation representation)
		{
			Console.WriteLine("Job finished: " + representation.JobClassType.Name);
		}
	}
}
using System;
using Simplify.DI;
using Simplify.Scheduler.IntegrationTester.Setup;

namespace Simplify.Scheduler.IntegrationTester
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// IOC container setup

			DIContainer.Current.RegisterAll()
				.Verify();

			// Using scheduler

			using (var scheduler = new MultitaskScheduler())
			{
				scheduler.OnJobStart += HandlerOnJobStart;
				scheduler.OnJobFinish += HandlerOnJobFinish;

				scheduler.AddJob<OneSecondStepProcessor>(IocRegistrations.Configuration);
				scheduler.AddJob<TwoSecondStepProcessor>(IocRegistrations.Configuration, startupArgs: "Hello world!!!");
				scheduler.AddJob<OneMinuteStepCrontabProcessor>(IocRegistrations.Configuration);
				scheduler.AddJob<TwoParallelTasksProcessor>(IocRegistrations.Configuration, invokeMethodName: "Execute");
				scheduler.AddBasicJob<BasicTaskProcessor>();

				if (scheduler.Start(args))
					return;
			}

			// Testing some processors without scheduler
			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}

		private static void HandlerOnJobStart(Jobs.ISchedulerJobRepresentation representation) => Console.WriteLine("Job started: " + representation.JobClassType.Name);

		private static void HandlerOnJobFinish(Jobs.ISchedulerJobRepresentation representation) => Console.WriteLine("Job finished: " + representation.JobClassType.Name);
	}
}
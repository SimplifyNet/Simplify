using System.Diagnostics;
using Simplify.DI;
using Simplify.WindowsServices.IntegrationTester.Setup;

namespace Simplify.WindowsServices.IntegrationTester
{
	internal class Program
	{
		private static void Main(string[] args)
		{
#if DEBUG
			// Run debugger
			Debugger.Launch();
#endif

			DIContainer.Current.RegisterAll()
				.Verify();

			using (var handler = new MultitaskServiceHandler())
			{
				handler.OnJobStart += HandlerOnJobStart;
				handler.OnJobFinish += HandlerOnJobFinish;

				handler.AddJob<OneSecondStepProcessor>("OneSecondStepProcessor");
				handler.AddJob<TwoSecondStepProcessor>(IocRegistrations.Configuration, startupArgs: "Hello world!!!");
				handler.AddJob<OneMinuteStepCrontabProcessor>(IocRegistrations.Configuration, "OneMinuteStepCrontabProcessor", automaticallyRegisterUserType: true);
				handler.AddJob<TwoParallelTasksProcessor>(invokeMethodName: "Execute");
				handler.AddBasicJob<BasicTaskProcessor>();

				if (handler.Start(args))
					return;
			}

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}

		private static void HandlerOnJobStart(Jobs.IServiceJobRepresentation representation) => Trace.WriteLine("Job started: " + representation.JobClassType.Name);

		private static void HandlerOnJobFinish(Jobs.IServiceJobRepresentation representation) => Trace.WriteLine("Job finished: " + representation.JobClassType.Name);
	}
}
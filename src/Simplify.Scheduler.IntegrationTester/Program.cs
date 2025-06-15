using Simplify.DI;
using Simplify.Scheduler;
using Simplify.Scheduler.IntegrationTester;
using Simplify.Scheduler.IntegrationTester.Diagnostics;
using Simplify.Scheduler.IntegrationTester.Setup;

// IOC container setup

DIContainer.Current
	.RegisterAll()
	.Verify();

// Using scheduler

using var scheduler = new MultitaskScheduler();

scheduler.OnJobStart += HandlerDiagnostics.HandlerOnJobStart;
scheduler.OnJobFinish += HandlerDiagnostics.HandlerOnJobFinish;

scheduler.AddJob<OneSecondStepProcessor>(IocRegistrations.Configuration);
scheduler.AddJob<TwoSecondStepProcessor>(IocRegistrations.Configuration, startupArgs: "Hello world!!!");
scheduler.AddJob<OneMinuteStepCrontabProcessor>(IocRegistrations.Configuration);
scheduler.AddJob<TwoParallelTasksProcessor>(IocRegistrations.Configuration, invokeMethodName: "Execute");
scheduler.AddBasicJob<BasicTaskProcessorAsync>();

if (await scheduler.StartAsync(args))
	return;


// Testing some processors without scheduler

using var scope = DIContainer.Current.BeginLifetimeScope();

await scope.Resolver.Resolve<BasicTaskProcessorAsync>().Run();

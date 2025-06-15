using System;
using Simplify.DI;
using Simplify.Scheduler;
using Simplify.Scheduler.Jobs;
using Simplify.Scheduler.SimpleApp;
using Simplify.Scheduler.SimpleApp.Setup;

// IOC container setup

DIContainer.Current
	.RegisterAll()
	.Verify();

// Using scheduler

using var scheduler = new MultitaskScheduler();

scheduler.OnJobStart += (ISchedulerJobRepresentation representation) => Console.WriteLine("Job started: " + representation.JobClassType.Name);
scheduler.OnJobFinish += (ISchedulerJobRepresentation representation) => Console.WriteLine("Job finished: " + representation.JobClassType.Name);

scheduler.AddJob<PeriodicalProcessor>(IocRegistrations.Configuration);

if (await scheduler.StartAsync(args))
	return;

// Testing without scheduler
using (var scope = DIContainer.Current.BeginLifetimeScope())
	scope.Resolver.Resolve<PeriodicalProcessor>().Run();

using System;

namespace Simplify.Scheduler.IntegrationTester.Diagnostics;

public static class HandlerDiagnostics
{
	public static void HandlerOnJobStart(Jobs.ISchedulerJobRepresentation representation) => Console.WriteLine("Job started: " + representation.JobClassType.Name);

	public static void HandlerOnJobFinish(Jobs.ISchedulerJobRepresentation representation) => Console.WriteLine("Job finished: " + representation.JobClassType.Name);
}

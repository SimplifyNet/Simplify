namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represent job related events handler
	/// </summary>
	/// <param name="representation">The representation.</param>
	public delegate void JobEventHandler(ISchedulerJobRepresentation representation);
}
namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represent basic scheduler job
	/// </summary>
	public interface ISchedulerJob : ISchedulerJobRepresentation
	{
		/// <summary>
		/// Starts this job timer.
		/// </summary>
		void Start();

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		void Stop();
	}
}
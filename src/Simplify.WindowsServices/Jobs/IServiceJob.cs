namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Represent basic service job
	/// </summary>
	public interface IServiceJob : IServiceJobRepresentation
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
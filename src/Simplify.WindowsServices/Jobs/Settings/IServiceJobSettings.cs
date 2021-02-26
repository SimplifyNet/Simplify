﻿namespace Simplify.WindowsServices.Jobs.Settings
{
	/// <summary>
	/// Represent service job settings
	/// </summary>
	public interface IServiceJobSettings
	{
		/// <summary>
		/// Gets the crontab expression.
		/// </summary>
		/// <value>
		/// The crontab expression.
		/// </value>
		string? CrontabExpression { get; }

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		int ProcessingInterval { get; }

		/// <summary>
		/// Gets a value indicating whether GC.Collect will be executed on on task finish.
		/// </summary>
		bool CleanupOnTaskFinish { get; }

		/// <summary>
		/// Gets the maximum allowed parallel tasks of this job.
		/// </summary>
		int MaximumParallelTasksCount { get; }
	}
}
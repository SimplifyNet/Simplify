namespace Simplify.Scheduler.Jobs.Settings.Impl;

/// <summary>
/// Provides custom scheduler job settings
/// </summary>
/// <seealso cref="SchedulerJobSettings" />
public class CustomSchedulerJobSettings : SchedulerJobSettings
{
	/// <summary>
	/// Adds scheduler settings builder
	/// </summary>
	public class Builder
	{
		private string? _crontabExpression;
		private int? _processingInterval;
		private bool? _cleanupOnTaskFinish;
		private int? _maximumParallelTasksCount;

		/// <summary>
		/// Builds the element.
		/// </summary>
		/// <returns></returns>
		public CustomSchedulerJobSettings Build()
		{
			return new()
			{
				CrontabExpression = _crontabExpression,
				ProcessingInterval = _processingInterval ?? 60,
				CleanupOnTaskFinish = _cleanupOnTaskFinish ?? true,
				MaximumParallelTasksCount = _maximumParallelTasksCount ?? 1
			};
		}

		/// <summary>
		/// Adds the crontab expression.
		/// </summary>
		/// <param name="crontabExpression">The crontab expression.</param>
		public Builder WithCrontabExpression(string? crontabExpression)
		{
			_crontabExpression = crontabExpression;

			return this;
		}

		/// <summary>
		/// Adds the processing interval.
		/// </summary>
		/// <param name="processingInterval">The processing interval.</param>
		public Builder WithProcessingInterval(int processingInterval)
		{
			_processingInterval = processingInterval;

			return this;
		}

		/// <summary>
		/// Adds the processing interval.
		/// </summary>
		/// <param name="cleanupOnTaskFinish">if set to <c>true</c> then GC.Collect will be executed on on task finish..</param>
		public Builder WithCleanupOnTaskFinish(bool cleanupOnTaskFinish)
		{
			_cleanupOnTaskFinish = cleanupOnTaskFinish;

			return this;
		}

		/// <summary>
		/// Adds the processing interval.
		/// </summary>
		/// <param name="maximumParallelTasksCount">The maximum allowed parallel tasks of this job.</param>
		public Builder WithMaximumParallelTasksCount(int maximumParallelTasksCount)
		{
			_maximumParallelTasksCount = maximumParallelTasksCount;

			return this;
		}
	}
}
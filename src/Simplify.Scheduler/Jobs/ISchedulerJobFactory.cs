using Microsoft.Extensions.Configuration;
using Simplify.Scheduler.Jobs.Crontab;
using Simplify.Scheduler.Jobs.Settings;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represent scheduler jobs factory
	/// </summary>
	public interface ISchedulerJobFactory
	{
		/// <summary>
		/// Creates the basic scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		ISchedulerJob CreateJob<T>(string invokeMethodName, object? startupArgs);

		/// <summary>
		/// Creates the crontab based scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		ICrontabSchedulerJob CreateCrontabJob<T>(IConfiguration configuration,
			string? configurationSectionName,
			string invokeMethodName,
			object? startupArgs);

		/// <summary>
		/// Creates the crontab based scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		ICrontabSchedulerJob CreateCrontabJob<T>(ISchedulerJobSettings settings,
			string invokeMethodName,
			object? startupArgs);
	}
}
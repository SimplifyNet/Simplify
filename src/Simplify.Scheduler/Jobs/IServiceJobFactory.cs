﻿using Microsoft.Extensions.Configuration;
using Simplify.Scheduler.Jobs.Crontab;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represent service jobs factory
	/// </summary>
	public interface IServiceJobFactory
	{
		/// <summary>
		/// Creates the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		IServiceJob CreateServiceJob<T>(string invokeMethodName,
			object startupArgs);

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		ICrontabServiceJob CreateCrontabServiceJob<T>(IConfiguration configuration,
			string configurationSectionName,
			string invokeMethodName,
			object startupArgs);
	}
}
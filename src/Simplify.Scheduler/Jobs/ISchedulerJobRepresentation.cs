using System;
using System.Reflection;

namespace Simplify.Scheduler.Jobs;

/// <summary>
/// Represent scheduler job information and args
/// </summary>
public interface ISchedulerJobRepresentation
{
	/// <summary>
	/// Gets the type of the job class.
	/// </summary>
	/// <value>
	/// The type of the job class.
	/// </value>
	Type JobClassType { get; }

	/// <summary>
	/// Gets the invoke method information.
	/// </summary>
	/// <value>
	/// The invoke method information.
	/// </value>
	MethodInfo InvokeMethodInfo { get; }

	/// <summary>
	/// Gets the type of the invoke method parameter.
	/// </summary>
	/// <value>
	/// The type of the invoke method parameter.
	/// </value>
	InvokeMethodParameterType InvokeMethodParameterType { get; }

	/// <summary>
	/// Gets the job arguments.
	/// </summary>
	/// <value>
	/// The job arguments.
	/// </value>
	IJobArgs JobArgs { get; }
}
﻿using System;
using System.Linq;
using System.Reflection;

namespace Simplify.WindowsServices.Jobs;

/// <summary>
/// Provides basic service job
/// </summary>
/// <seealso cref="IServiceJob" />
public class ServiceJob<T> : IServiceJob
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ServiceJob{T}" /> class.
	/// </summary>
	/// <param name="invokeMethodName">Name of the invoke method.</param>
	/// <param name="args">The arguments.</param>
	/// <exception cref="ArgumentNullException">invokeMethodName</exception>
	/// <exception cref="ServiceInitializationException"></exception>
	public ServiceJob(string invokeMethodName, IJobArgs args)
	{
		if (invokeMethodName == null) throw new ArgumentNullException(nameof(invokeMethodName));

		JobClassType = typeof(T);
		JobArgs = args ?? throw new ArgumentNullException(nameof(args));

		InvokeMethodInfo = BuildInvokeMethodInfo(invokeMethodName);
	}

	/// <summary>
	/// Gets the type of the job class.
	/// </summary>
	/// <value>
	/// The type of the job class.
	/// </value>
	public Type JobClassType { get; }

	/// <summary>
	/// Gets the invoke method information.
	/// </summary>
	/// <value>
	/// The invoke method information.
	/// </value>
	public MethodInfo InvokeMethodInfo { get; private set; }

	/// <summary>
	/// Gets the type of the invoke method parameter.
	/// </summary>
	/// <value>
	/// The type of the invoke method parameter.
	/// </value>
	public InvokeMethodParameterType InvokeMethodParameterType { get; private set; }

	/// <summary>
	/// Gets the job arguments.
	/// </summary>
	/// <value>
	/// The job arguments.
	/// </value>
	public IJobArgs JobArgs { get; }

	/// <summary>
	/// Starts this job timer.
	/// </summary>
	/// <exception cref="ServiceInitializationException"></exception>
	public virtual void Start()
	{
	}

	/// <summary>
	/// Stops and disposes job timer.
	/// </summary>
	public virtual void Stop()
	{
	}

	private MethodInfo BuildInvokeMethodInfo(string invokeMethodName)
	{
		var invokeMethodInfo = JobClassType.GetMethod(invokeMethodName) ?? throw new ServiceInitializationException($"Method {invokeMethodName} not found in class {JobClassType.Name}");

		var methodParameters = invokeMethodInfo.GetParameters();

		if (!methodParameters.Any())
		{
			InvokeMethodParameterType = InvokeMethodParameterType.Parameterless;
			return invokeMethodInfo;
		}

		if (methodParameters[0].ParameterType == typeof(string))
			InvokeMethodParameterType = InvokeMethodParameterType.ServiceName;

		if (methodParameters[0].ParameterType == typeof(IJobArgs))
			InvokeMethodParameterType = InvokeMethodParameterType.Args;

		return invokeMethodInfo;
	}
}
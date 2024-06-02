using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Scheduler.Jobs;
using Simplify.Scheduler.Jobs.Crontab;
using Simplify.Scheduler.Jobs.Settings;
using Simplify.System;

namespace Simplify.Scheduler;

/// <summary>
/// Provides jobs execution handler
/// </summary>
public abstract class SchedulerJobsHandler : IDisposable
{
	private readonly IList<ISchedulerJob> _jobs = new List<ISchedulerJob>();
	private readonly IList<ICrontabSchedulerJobTask> _workingJobsTasks = new List<ICrontabSchedulerJobTask>();
	private readonly IDictionary<ISchedulerJobRepresentation, ILifetimeScope> _workingBasicJobs = new Dictionary<ISchedulerJobRepresentation, ILifetimeScope>();

	private long _jobTaskID;
	private ISchedulerJobFactory? _schedulerJobFactory;

	/// <summary>
	/// Initializes a new instance of the <see cref="MultitaskScheduler" /> class.
	/// </summary>
	protected SchedulerJobsHandler()
	{
		var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
		AppName = assemblyInfo.Title;
	}

	/// <summary>
	/// Occurs when exception thrown.
	/// </summary>
	public event SchedulerExceptionEventHandler? OnException;

	/// <summary>
	/// Occurs when the job start.
	/// </summary>
	public event JobEventHandler? OnJobStart;

	/// <summary>
	/// Occurs when job is finished.
	/// </summary>
	public event JobEventHandler? OnJobFinish;

	/// <summary>
	/// Gets a value indicating whether handler shutdown is in process.
	/// </summary>
	/// <value>
	///   <c>true</c> if handler shutdown is in process; otherwise, <c>false</c>.
	/// </value>
	public bool ShutdownInProcess { get; private set; }

	/// <summary>
	/// Gets the name of the application.
	/// </summary>
	/// <value>
	/// The name of the application.
	/// </value>
	public string AppName { get; protected set; }

	/// <summary>
	/// Gets or sets the scheduler job factory.
	/// </summary>
	/// <value>
	/// The scheduler job factory.
	/// </value>
	/// <exception cref="ArgumentNullException">value</exception>
	public ISchedulerJobFactory SchedulerJobFactory
	{
		get => _schedulerJobFactory ??= new SchedulerJobFactory(AppName);
		set => _schedulerJobFactory = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Adds the job.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="configuration">The configuration.</param>
	/// <param name="configurationSectionName">Name of the configuration section.</param>
	/// <param name="invokeMethodName">Name of the invoke method.</param>
	/// <param name="startupArgs">The startup arguments.</param>
	public void AddJob<T>(IConfiguration configuration,
		string? configurationSectionName = null,
		string invokeMethodName = "Run",
		object? startupArgs = null)
		where T : class
	{
		var job = SchedulerJobFactory.CreateCrontabJob<T>(configuration, configurationSectionName, invokeMethodName, startupArgs);

		InitializeJob(job);
	}

	/// <summary>
	/// Adds the job.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="settings">The settings.</param>
	/// <param name="invokeMethodName">Name of the invoke method.</param>
	/// <param name="startupArgs">The startup arguments.</param>
	public void AddJob<T>(ISchedulerJobSettings settings,
		string invokeMethodName = "Run",
		object? startupArgs = null)
		where T : class
	{
		var job = SchedulerJobFactory.CreateCrontabJob<T>(settings, invokeMethodName, startupArgs);

		InitializeJob(job);
	}

	/// <summary>
	/// Adds the basic scheduler job.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="invokeMethodName">Name of the invoke method.</param>
	/// <param name="startupArgs">The startup arguments.</param>
	public void AddBasicJob<T>(string invokeMethodName = "Run",
		object? startupArgs = null)
		where T : class
	{
		var job = SchedulerJobFactory.CreateJob<T>(invokeMethodName, startupArgs);

		_jobs.Add(job);
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Called when scheduler is started, main execution starting point.
	/// </summary>
	protected async Task StartJobsAsync()
	{
		Console.WriteLine("Starting Scheduler jobs...");

		foreach (var job in _jobs)
		{
			job.Start();

			if (!(job is ICrontabSchedulerJob))
				await RunBasicJobAsync(job);
		}

		Console.WriteLine("Scheduler jobs started.");
	}

	/// <summary>
	/// Called when scheduler is about to stop, main stopping point
	/// </summary>
	protected void StopJobs()
	{
		Console.WriteLine("Scheduler stopping, waiting for jobs to finish...");

		ShutdownInProcess = true;
		Task[] itemsToWait;

		lock (_workingJobsTasks)
			itemsToWait = _workingJobsTasks.Select(x => x.Task).ToArray();

		Task.WaitAll(itemsToWait);

		Console.WriteLine("All jobs finished.");
	}

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	// ReSharper disable once FlagArgument
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		foreach (var basicJobItem in _workingBasicJobs)
		{
			OnJobFinish?.Invoke(basicJobItem.Key);

			basicJobItem.Value.Dispose();
		}
	}

	private void InitializeJob(ICrontabSchedulerJob job)
	{
		job.OnCronTimerTick += OnCronTimerTick;
		job.OnStartWork += OnStartWork;

		_jobs.Add(job);
	}

	private void OnCronTimerTick(object? state)
	{
		if (state is null)
			throw new ArgumentNullException(nameof(state));

		var job = (ICrontabSchedulerJob)state;

		if (job.CrontabProcessor == null)
			throw new InvalidOperationException($"{nameof(job.CrontabProcessor)} is null");

		if (!job.CrontabProcessor.IsMatching())
			return;

		job.CrontabProcessor.CalculateNextOccurrences();

		OnStartWork(state);
	}

	private void OnStartWork(object? state)
	{
		if (state is null)
			throw new ArgumentNullException(nameof(state));

		var job = (ICrontabSchedulerJob)state;

		lock (_workingJobsTasks)
		{
			if (ShutdownInProcess || _workingJobsTasks.Count(x => x.Job == job) >= job.Settings.MaximumParallelTasksCount)
				return;

			_jobTaskID++;

			_workingJobsTasks.Add(new CrontabSchedulerJobTask(_jobTaskID, job,
				Task.Factory.StartNew(Run, new Tuple<long, ICrontabSchedulerJob>(_jobTaskID, job)).Unwrap()));
		}
	}

	#region Execution

	private async Task Run(object? state)
	{
		if (state is null)
			throw new ArgumentNullException(nameof(state));

		var (jobTaskID, job) = (Tuple<long, ICrontabSchedulerJob>)state;

		try
		{
			await RunScoped(job);
		}
		catch (Exception e)
		{
			if (OnException != null)
				OnException(new SchedulerExceptionArgs(AppName, e));
			else
				throw;
		}
		finally
		{
			FinalizeJob(jobTaskID, job);
		}
	}

	private async Task RunScoped(ISchedulerJobRepresentation job)
	{
		using var scope = DIContainer.Current.BeginLifetimeScope();

		var jobObject = scope.Resolver.Resolve(job.JobClassType);

		OnJobStart?.Invoke(job);

		await InvokeJobMethodAsync(job, jobObject);

		OnJobFinish?.Invoke(job);
	}

	private async Task RunBasicJobAsync(ISchedulerJobRepresentation job)
	{
		try
		{
			var scope = DIContainer.Current.BeginLifetimeScope();

			var jobObject = scope.Resolver.Resolve(job.JobClassType);

			OnJobStart?.Invoke(job);

			await InvokeJobMethodAsync(job, jobObject);

			_workingBasicJobs.Add(job, scope);
		}
		catch (Exception e)
		{
			if (OnException != null)
				OnException(new SchedulerExceptionArgs(AppName, e));
			else
				throw;
		}
	}

	private Task InvokeJobMethodAsync(ISchedulerJobRepresentation job, object jobObject)
	{
		var result = job.InvokeMethodParameterType switch
		{
			InvokeMethodParameterType.Parameterless => job.InvokeMethodInfo.Invoke(jobObject, null),
			InvokeMethodParameterType.AppName => job.InvokeMethodInfo.Invoke(jobObject, new object[] { AppName }),
			InvokeMethodParameterType.Args => job.InvokeMethodInfo.Invoke(jobObject, new object[] { job.JobArgs }),
			_ => throw new InvalidEnumArgumentException(nameof(job.InvokeMethodParameterType)),
		};
		if (result is Task task)
			return task;

		return Task.CompletedTask;
	}

	private void FinalizeJob(long jobTaskID, ICrontabSchedulerJob job)
	{
		if (job.Settings.CleanupOnTaskFinish)
			GC.Collect();

		lock (_workingJobsTasks)
			_workingJobsTasks.Remove(_workingJobsTasks.Single(x => x.ID == jobTaskID));
	}

	#endregion Execution
}
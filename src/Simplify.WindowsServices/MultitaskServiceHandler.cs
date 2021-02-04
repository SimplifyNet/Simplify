﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.System;
using Simplify.WindowsServices.CommandLine;
using Simplify.WindowsServices.Jobs;
using Simplify.WindowsServices.Jobs.Crontab;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instances specified in added jobs and launches them in separated thread
	/// </summary>
	public class MultitaskServiceHandler : ServiceBase
	{
		private readonly IList<IServiceJob> _jobs = new List<IServiceJob>();
		private readonly IList<ICrontabServiceJobTask> _workingJobsTasks = new List<ICrontabServiceJobTask>();
		private readonly IDictionary<IServiceJobRepresentation, ILifetimeScope> _workingBasicJobs = new Dictionary<IServiceJobRepresentation, ILifetimeScope>();

		private long _jobTaskID;
		private IServiceJobFactory _serviceJobFactory;
		private ICommandLineProcessor _commandLineProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultitaskServiceHandler" /> class.
		/// </summary>
		public MultitaskServiceHandler()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

		/// <summary>
		/// Occurs when the job start.
		/// </summary>
		public event JobEventHandler OnJobStart;

		/// <summary>
		/// Occurs when job is finished.
		/// </summary>
		public event JobEventHandler OnJobFinish;

		/// <summary>
		/// Gets a value indicating whether handler shutdown is in process.
		/// </summary>
		/// <value>
		///   <c>true</c> if handler shutdown is in process; otherwise, <c>false</c>.
		/// </value>
		public bool ShutdownInProcess { get; private set; }

		/// <summary>
		/// Gets or sets the service job factory.
		/// </summary>
		/// <value>
		/// The service job factory.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceJobFactory ServiceJobFactory
		{
			get => _serviceJobFactory ?? (_serviceJobFactory = new ServiceJobFactory(ServiceName));
			set => _serviceJobFactory = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets or sets the current command line processor.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public ICommandLineProcessor CommandLineProcessor
		{
			get => _commandLineProcessor ?? (_commandLineProcessor = new CommandLineProcessor());
			set => _commandLineProcessor = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddJob<T>(string configurationSectionName = null,
			string invokeMethodName = "Run",
			bool automaticallyRegisterUserType = false,
			object startupArgs = null)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateCrontabServiceJob<T>(configurationSectionName, invokeMethodName, startupArgs);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddJob<T>(IConfiguration configuration,
			string configurationSectionName = null,
			string invokeMethodName = "Run",
			bool automaticallyRegisterUserType = false,
			object startupArgs = null)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateCrontabServiceJob<T>(configuration, configurationSectionName, invokeMethodName, startupArgs);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(bool automaticallyRegisterUserType)
			where T : class
		{
			AddJob<T>(null, "Run", automaticallyRegisterUserType);
		}

		/// <summary>
		/// Adds the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(IConfiguration configuration,
			bool automaticallyRegisterUserType)
			where T : class
		{
			AddJob<T>(configuration, null, "Run", automaticallyRegisterUserType);
		}

		/// <summary>
		/// Adds the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddBasicJob<T>(bool automaticallyRegisterUserType = false,
			string invokeMethodName = "Run",
			object startupArgs = null)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateServiceJob<T>(invokeMethodName, startupArgs);

			_jobs.Add(job);
		}

		/// <summary>
		/// Starts the windows-service.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public bool Start(string[] args = null)
		{
			var commandLineProcessResult = CommandLineProcessor.ProcessCommandLineArguments(args);

			switch (commandLineProcessResult)
			{
				case ProcessCommandLineResult.SkipServiceStart:
					return false;

				case ProcessCommandLineResult.NoArguments:
					ServiceBase.Run(this);
					break;
			}

			return true;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase" />.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				foreach (var basicJobItem in _workingBasicJobs)
				{
					OnJobFinish?.Invoke(basicJobItem.Key);

					basicJobItem.Value.Dispose();
				}

			base.Dispose(disposing);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(string[] args)
		{
			foreach (var job in _jobs)
			{
				job.Start();

				if (!(job is ICrontabServiceJob))
					RunBasicJob(job);
			}

			base.OnStart(args);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop()
		{
			ShutdownInProcess = true;
			Task[] itemsToWait;

			lock (_workingJobsTasks)
				itemsToWait = _workingJobsTasks.Select(x => x.Task).ToArray();

			Task.WaitAll(itemsToWait);

			base.OnStop();
		}

		private void InitializeJob(ICrontabServiceJob job)
		{
			job.OnCronTimerTick += OnCronTimerTick;
			job.OnStartWork += OnStartWork;

			_jobs.Add(job);
		}

		private void OnCronTimerTick(object state)
		{
			var job = (ICrontabServiceJob)state;

			if (!job.CrontabProcessor.IsMatching())
				return;

			job.CrontabProcessor.CalculateNextOccurrences();

			OnStartWork(state);
		}

		private void OnStartWork(object state)
		{
			var job = (ICrontabServiceJob)state;

			lock (_workingJobsTasks)
			{
				if (ShutdownInProcess || _workingJobsTasks.Count(x => x.Job == job) >= job.Settings.MaximumParallelTasksCount)
					return;

				_jobTaskID++;

				_workingJobsTasks.Add(new CrontabServiceJobTask(_jobTaskID, job,
					Task.Factory.StartNew(Run, new Tuple<long, ICrontabServiceJob>(_jobTaskID, job))));
			}
		}

		#region Execution

		/// <summary>
		/// Separate thread entry point for job processing
		/// </summary>
		/// <param name="state">The state.</param>
		private void Run(object state)
		{
			var (jobTaskID, job) = (Tuple<long, ICrontabServiceJob>)state;

			try
			{
				RunScoped(job);
			}
			catch (Exception e)
			{
				if (OnException != null)
					OnException(new ServiceExceptionArgs(ServiceName, e));
				else
					throw;
			}
			finally
			{
				FinalizeJob(jobTaskID, job);
			}
		}

		private void RunScoped(IServiceJobRepresentation job)
		{
			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				var jobObject = scope.Resolver.Resolve(job.JobClassType);

				OnJobStart?.Invoke(job);

				InvokeJobMethod(job, jobObject);

				OnJobFinish?.Invoke(job);
			}
		}

		private void RunBasicJob(IServiceJobRepresentation job)
		{
			try
			{
				var scope = DIContainer.Current.BeginLifetimeScope();

				var jobObject = scope.Resolver.Resolve(job.JobClassType);

				OnJobStart?.Invoke(job);

				InvokeJobMethod(job, jobObject);

				_workingBasicJobs.Add(job, scope);
			}
			catch (Exception e)
			{
				if (OnException != null)
					OnException(new ServiceExceptionArgs(ServiceName, e));
				else
					throw;
			}
		}

		private void InvokeJobMethod(IServiceJobRepresentation job, object jobObject)
		{
			switch (job.InvokeMethodParameterType)
			{
				case InvokeMethodParameterType.Parameterless:
					job.InvokeMethodInfo.Invoke(jobObject, null);
					break;

				case InvokeMethodParameterType.ServiceName:
					job.InvokeMethodInfo.Invoke(jobObject, new object[] { ServiceName });
					break;

				case InvokeMethodParameterType.Args:
					job.InvokeMethodInfo.Invoke(jobObject, new object[] { job.JobArgs });
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void FinalizeJob(long jobTaskID, ICrontabServiceJob job)
		{
			if (job.Settings.CleanupOnTaskFinish)
				GC.Collect();

			lock (_workingJobsTasks)
				_workingJobsTasks.Remove(_workingJobsTasks.Single(x => x.ID == jobTaskID));
		}

		#endregion Execution
	}
}
﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Simplify.Scheduler.CommandLine;
using Simplify.System;

namespace Simplify.Scheduler;

/// <summary>
/// Provides class which periodically creates a class instances specified in added jobs and launches them in separated thread, optimized to work as a console application
/// </summary>
public class MultitaskScheduler : SchedulerJobsHandler
{
	private readonly AutoResetEvent _closing = new(false);

	private ICommandLineProcessor? _commandLineProcessor;

	/// <summary>
	/// Initializes a new instance of the <see cref="MultitaskScheduler" /> class.
	/// </summary>
	public MultitaskScheduler()
	{
		var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
		AppName = assemblyInfo.Title;

		Console.CancelKeyPress += StopJobs;
	}

	/// <summary>
	/// Gets or sets the current command line processor.
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	public ICommandLineProcessor CommandLineProcessor
	{
		get => _commandLineProcessor ??= new CommandLineProcessor();
		set => _commandLineProcessor = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Starts the scheduler asynchronously.
	/// </summary>
	/// <param name="args">The arguments.</param>
	public async Task<bool> StartAsync(string[]? args = null)
	{
		var commandLineProcessResult = CommandLineProcessor.ProcessCommandLineArguments(args);

		switch (commandLineProcessResult)
		{
			case ProcessCommandLineResult.SkipSchedulerStart:
				return false;

			case ProcessCommandLineResult.NoArguments:
				await StartAsync();
				break;

			case ProcessCommandLineResult.UndefinedParameters:
				break;

			case ProcessCommandLineResult.CommandLineActionExecuted:
				break;

			default:
				throw new InvalidEnumArgumentException(nameof(commandLineProcessResult));
		}

		return true;
	}

	/// <summary>
	/// Starts the scheduler.
	/// </summary>
	/// <param name="args">The arguments.</param>
	public bool Start(string[]? args = null) =>
		StartAsync(args)
		.ConfigureAwait(false)
		.GetAwaiter()
		.GetResult();

	/// <summary>
	/// Called when scheduler is about to stop, main stopping point
	/// </summary>
	protected void StopJobs(object? sender, ConsoleCancelEventArgs args)
	{
		StopJobs();

		args.Cancel = true;
		_closing.Set();
	}

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	// ReSharper disable once FlagArgument
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_closing.Dispose();
	}

	private async Task StartAsync()
	{
		await StartJobsAsync();

		Console.WriteLine("Scheduler started. Press Ctrl + C to shut down.");

		_closing.WaitOne();
	}
}
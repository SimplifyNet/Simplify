﻿using System;
using System.Configuration.Install;
using System.Reflection;

namespace Simplify.WindowsServices.CommandLine;

/// <summary>
/// Provides windows-service installation controller
/// </summary>
/// <seealso cref="IInstallationController" />
public class InstallationController : IInstallationController
{
	/// <summary>
	/// Installs the service.
	/// </summary>
	public virtual void InstallService() => ManagedInstallerClass.InstallHelper(new[] { "/LogFile=", "/LogToConsole=true", GetEntryAssemblyLocation() });

	/// <summary>
	/// Uninstalls the service.
	/// </summary>
	public virtual void UninstallService() => ManagedInstallerClass.InstallHelper(new[] { "/u", "/LogFile=", "/LogToConsole=true", GetEntryAssemblyLocation() });

	/// <summary>
	/// Gets the entry assembly location.
	/// </summary>
	/// <returns></returns>
	protected string GetEntryAssemblyLocation() => Assembly.GetEntryAssembly()?.Location ?? throw new InvalidOperationException("Entry assembly location is null");
}
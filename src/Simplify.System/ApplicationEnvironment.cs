using System;

namespace Simplify.System;

/// <summary>
/// Provides application environment information
/// </summary>
public static class ApplicationEnvironment
{
	/// <summary>
	/// The default environment name
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public const string DefaultEnvironmentName = "Production";

	/// <summary>
	/// The environment variable name
	/// </summary>
	public const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

	private static readonly object NameLock = new();

	private static string? _name;

	/// <summary>
	/// Checks the application environment is Production
	/// </summary>
	public static bool IsProduction => Name == "Production";

	/// <summary>
	/// Checks the application environment is Development
	/// </summary>
	public static bool IsDevelopment => Name == "Development";

	/// <summary>
	/// Checks the application environment is Staging
	/// </summary>
	public static bool IsStaging => Name == "Staging";

	/// <summary>
	/// Gets or sets the current environment name.
	/// </summary>
	/// <value>
	/// The current environment name.
	/// </value>
	public static string Name
	{
		get
		{
			if (_name != null) return _name;

			lock (NameLock)
				_name ??= Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? DefaultEnvironmentName;

			return _name;
		}

		set => _name = value ?? throw new ArgumentNullException(nameof(value));
	}
}
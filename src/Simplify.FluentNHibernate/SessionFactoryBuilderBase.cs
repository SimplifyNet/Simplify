using System;
using Microsoft.Extensions.Configuration;
using FluentNHibernate.Cfg;
using NHibernate;

namespace Simplify.FluentNHibernate;

/// <summary>
/// Base class for session factory builders
/// </summary>
public abstract class SessionFactoryBuilderBase : IDisposable
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SessionFactoryBuilderBase"/> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="configSectionName">Name of the configuration section.</param>
	/// <exception cref="ArgumentException">Value cannot be null or empty. - configSectionName</exception>
	/// <exception cref="ArgumentNullException">configuration</exception>
	protected SessionFactoryBuilderBase(IConfiguration configuration, string configSectionName)
	{
		if (string.IsNullOrEmpty(configSectionName)) throw new ArgumentException("Value cannot be null or empty.", nameof(configSectionName));

		Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		ConfigSectionName = configSectionName;
	}

	/// <summary>
	/// Gets the connection string.
	/// </summary>
	/// <value>
	/// The connection string.
	/// </value>
	public string? ConnectionString { get; private set; }

	/// <summary>
	/// Gets or sets the session factory.
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public ISessionFactory? Instance { get; private set; }

	/// <summary>
	/// Gets the configuration.
	/// </summary>
	/// <value>
	/// The configuration.
	/// </value>
	protected IConfiguration Configuration { get; }

	/// <summary>
	/// Gets the name of the configuration section.
	/// </summary>
	/// <value>
	/// The name of the configuration section.
	/// </value>
	protected string ConfigSectionName { get; }

	/// <summary>
	/// Gets the fluent configuration.
	/// </summary>
	/// <value>
	/// The fluent configuration.
	/// </value>
	protected FluentConfiguration FluentConfiguration { get; } = Fluently.Configure();

	/// <summary>
	/// Builds the session factory.
	/// </summary>
	public virtual SessionFactoryBuilderBase Build()
	{
		var configuration = CreateConfiguration();

		FluentConfiguration.ExposeConfiguration(c =>
		{
			ConnectionString = c.GetProperty(NHibernate.Cfg.Environment.ConnectionString);
		});

		Instance = configuration.BuildSessionFactory();

		return this;
	}

	/// <summary>
	/// Creates the configuration.
	/// </summary>
	/// <returns></returns>
	public abstract FluentConfiguration CreateConfiguration();

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public virtual void Dispose() => Instance?.Dispose();
}
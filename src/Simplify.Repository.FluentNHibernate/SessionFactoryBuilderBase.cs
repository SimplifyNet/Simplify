using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Base class for session factory builders
	/// </summary>
	public abstract class SessionFactoryBuilderBase : IDisposable
	{
		/// <summary>
		/// Gets or sets the session factory.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public ISessionFactory Instance { get; protected set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		{
			Instance?.Dispose();
		}
	}
}
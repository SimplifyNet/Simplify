namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container scoped context handler
	/// </summary>
	public interface IDIContextHandler
	{
		/// <summary>
		/// Occurs when the lifetime scope is opened
		/// </summary>
		event BeginLifetimeScopeEventHandler OnBeginLifetimeScope;

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		ILifetimeScope BeginLifetimeScope();
	}
}
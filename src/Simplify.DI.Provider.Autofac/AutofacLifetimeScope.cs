namespace Simplify.DI.Provider.Autofac;

/// <summary>
/// Autofac DI provider lifetime scope implementation
/// </summary>
public class AutofacLifetimeScope : ILifetimeScope
{
	private readonly global::Autofac.ILifetimeScope _scope;

	/// <summary>
	/// Initializes a new instance of the <see cref="AutofacLifetimeScope"/> class.
	/// </summary>
	/// <param name="provider">The provider.</param>
	public AutofacLifetimeScope(AutofacDIProvider provider)
	{
		_scope = provider.Container.BeginLifetimeScope();

		Resolver = new AutofacDIResolver(_scope);
	}

	/// <summary>
	/// Gets the DI container resolver (should be used to resolve types when using scoping).
	/// </summary>
	/// <value>
	/// The DI container resolver (should be used to resolve types when using scoping).
	/// </value>
	public IDIResolver Resolver { get; }

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose() => _scope.Dispose();
}
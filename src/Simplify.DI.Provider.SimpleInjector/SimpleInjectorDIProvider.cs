using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Simplify.DI.Provider.SimpleInjector;

/// <summary>
/// Simple Injector DI container provider implementation
/// </summary>
public class SimpleInjectorDIProvider : IDIContainerProvider
{
	private Container? _container;

	/// <summary>
	/// Occurs when the lifetime scope is opened
	/// </summary>
	public event BeginLifetimeScopeEventHandler? OnBeginLifetimeScope;

	/// <summary>
	/// The IOC container
	/// </summary>
	public Container Container
	{
		get => _container ??= new Container
		{
			Options =
			{
				DefaultScopedLifestyle = new AsyncScopedLifestyle(),
				UseStrictLifestyleMismatchBehavior = true
			}
		};

		set => _container = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Resolves the specified service type.
	/// </summary>
	/// <param name="serviceType">Type of the service.</param>
	/// <returns></returns>
	public object Resolve(Type serviceType) => Container.GetInstance(serviceType);

	/// <summary>
	/// Registers the specified service type with corresponding implementation type.
	/// </summary>
	/// <param name="serviceType">Service type.</param>
	/// <param name="implementationType">Implementation type.</param>
	/// <param name="lifetimeType">Lifetime type of the registering services type.</param>
	public IDIRegistrator Register(Type serviceType, Type implementationType, LifetimeType lifetimeType)
	{
		switch (lifetimeType)
		{
			case LifetimeType.PerLifetimeScope:
				Container.Register(serviceType, implementationType, Lifestyle.Scoped);
				break;

			case LifetimeType.Singleton:
				Container.Register(serviceType, implementationType, Lifestyle.Singleton);
				break;

			case LifetimeType.Transient:
				Container.Register(serviceType, implementationType, Lifestyle.Transient);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(lifetimeType), lifetimeType, null);
		}

		return this;
	}

	/// <summary>
	/// Registers the specified service type using instance creation delegate.
	/// </summary>
	/// <param name="serviceType">Type of the service.</param>
	/// <param name="instanceCreator">The instance creator.</param>
	/// <param name="lifetimeType">Lifetime type of the registering type.</param>
	public IDIRegistrator Register(Type serviceType, Func<IDIResolver, object> instanceCreator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
	{
		switch (lifetimeType)
		{
			case LifetimeType.PerLifetimeScope:
				Container.Register(serviceType, () => instanceCreator(this), Lifestyle.Scoped);
				break;

			case LifetimeType.Singleton:
				Container.Register(serviceType, () => instanceCreator(this), Lifestyle.Singleton);
				break;

			case LifetimeType.Transient:
				Container.Register(serviceType, () => instanceCreator(this), Lifestyle.Transient);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(lifetimeType), lifetimeType, null);
		}

		return this;
	}

	/// <summary>
	/// Begins the lifetime scope.
	/// </summary>
	/// <returns></returns>
	public ILifetimeScope BeginLifetimeScope()
	{
		var scope = new SimpleInjectorLifetimeScope(this);

		OnBeginLifetimeScope?.Invoke(scope);

		return scope;
	}

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	public void Dispose() => _container?.Dispose();

	/// <summary>
	/// Performs container objects graph verification
	/// </summary>
	public void Verify() => Container.Verify();
}
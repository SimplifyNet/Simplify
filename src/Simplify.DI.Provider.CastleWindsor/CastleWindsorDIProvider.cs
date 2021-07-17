using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Simplify.DI.Provider.CastleWindsor
{
	/// <summary>
	/// Castle Windsor container provider implementation
	/// </summary>
	public class CastleWindsorDIProvider : IDIContainerProvider
	{
		private IWindsorContainer? _container;

		/// <summary>
		/// Occurs when the lifetime scope is opened
		/// </summary>
		public event BeginLifetimeScopeEventHandler? OnBeginLifetimeScope;

		/// <summary>
		/// The IOC container
		/// </summary>
		public IWindsorContainer Container
		{
			get => _container ??= new WindsorContainer();
			set => _container = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType) => Container.Resolve(serviceType);

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
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleScoped());
					break;

				case LifetimeType.Singleton:
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleSingleton());
					break;

				case LifetimeType.Transient:
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleTransient());
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(lifetimeType), lifetimeType, null);
			}

			return this;
		}

		/// <summary>
		/// Registers the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Type of the lifetime.</param>
		public IDIRegistrator Register(Type serviceType, Func<IDIResolver, object> instanceCreator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(() => instanceCreator(this)).LifestyleScoped());
					break;

				case LifetimeType.Singleton:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(() => instanceCreator(this)).LifestyleSingleton());
					break;

				case LifetimeType.Transient:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(() => instanceCreator(this)).LifestyleTransient());
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
			var scope = new CastleWindsorLifetimeScope(this);

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
		public void Verify() => Container.CheckForPotentiallyMisconfiguredComponents();
	}
}
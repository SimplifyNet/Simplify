using System;
using Autofac;

namespace Simplify.DI.Provider.Autofac
{
	/// <summary>
	/// Autofac DI container provider implementation
	/// </summary>
	public class AutofacDIProvider : IDIContainerProvider
	{
		private ContainerBuilder? _containerBuilder;
		private IContainer? _container;

		/// <summary>
		/// The IOC container builder
		/// </summary>
		public ContainerBuilder ContainerBuilder
		{
			get => _containerBuilder ??= new ContainerBuilder();

			set => _containerBuilder = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// The IOC container
		/// </summary>
		public IContainer Container
		{
			get => _container ??= ContainerBuilder.Build();

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
					ContainerBuilder.RegisterType(implementationType).As(serviceType).InstancePerLifetimeScope();
					break;

				case LifetimeType.Singleton:
					ContainerBuilder.RegisterType(implementationType).As(serviceType).SingleInstance();
					break;

				case LifetimeType.Transient:
					ContainerBuilder.RegisterType(implementationType).As(serviceType).InstancePerDependency();
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
					ContainerBuilder.Register(c => instanceCreator(new AutofacDIResolver(c))).As(serviceType).InstancePerLifetimeScope();
					break;

				case LifetimeType.Singleton:
					ContainerBuilder.Register(c => instanceCreator(new AutofacDIResolver(c))).As(serviceType).SingleInstance();
					break;

				case LifetimeType.Transient:
					ContainerBuilder.Register(c => instanceCreator(new AutofacDIResolver(c))).As(serviceType).InstancePerDependency();
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
		public ILifetimeScope BeginLifetimeScope() => new AutofacLifetimeScope(this);

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose() => _container?.Dispose();

		/// <summary>
		/// Performs container objects graph verification
		/// </summary>
		public void Verify() => throw new NotImplementedException();
	}
}
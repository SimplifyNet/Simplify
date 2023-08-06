﻿using System;

namespace Simplify.DI;

/// <summary>
/// Provides DI registrator extensions
/// </summary>
public static class DIRegistratorExtensions
{
	/// <summary>
	/// Registers the specified concrete type for resolve.
	/// </summary>
	/// <param name="registrator">The DI registrator.</param>
	/// <param name="concreteType">Concrete type.</param>
	/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
	public static IDIRegistrator Register(this IDIRegistrator registrator, Type concreteType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope) =>
		registrator.Register(concreteType, concreteType, lifetimeType);

	/// <summary>
	/// Registers the specified concrete type for resolve.
	/// </summary>
	/// <typeparam name="TConcrete">Concrete type.</typeparam>
	/// <param name="registrator">The DI registrator.</param>
	/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
	public static IDIRegistrator Register<TConcrete>(this IDIRegistrator registrator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		where TConcrete : class =>
		registrator.Register(typeof(TConcrete), typeof(TConcrete), lifetimeType);

	/// <summary>
	/// Registers the specified service type with corresponding implementation type.
	/// </summary>
	/// <typeparam name="TService">Service type.</typeparam>
	/// <param name="registrator">The DI registrator.</param>
	/// <param name="implementationType">Implementation type.</param>
	/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
	public static IDIRegistrator Register<TService>(this IDIRegistrator registrator, Type implementationType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope) =>
		registrator.Register(typeof(TService), implementationType, lifetimeType);

	/// <summary>
	/// Registers the specified service type for resolve with delegate for service implementation instance creation.
	/// </summary>
	/// <typeparam name="TService">Service type.</typeparam>
	/// <param name="registrator">The DI registrator.</param>
	/// <param name="instanceCreator">The instance creator.</param>
	/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
	public static IDIRegistrator Register<TService>(this IDIRegistrator registrator, Func<IDIResolver, TService> instanceCreator,
		LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		where TService : class =>
		registrator.Register(typeof(TService), instanceCreator, lifetimeType);

	/// <summary>
	/// Registers the specified service type with corresponding implementation type.
	/// </summary>
	/// <typeparam name="TService">Service type.</typeparam>
	/// <typeparam name="TImplementation">Implementation type.</typeparam>
	/// <param name="registrator">The DI registrator.</param>
	/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
	public static IDIRegistrator Register<TService, TImplementation>(this IDIRegistrator registrator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope) =>
		registrator.Register(typeof(TService), typeof(TImplementation), lifetimeType);
}
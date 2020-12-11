﻿using DryIoc;
using NUnit.Framework;
using Simplify.DI.Provider.DryIoc;
using Simplify.DI.TestsTypes;

namespace Simplify.DI.Tests
{
	[TestFixture]
	public class DIContainerProviderTests
	{
		private IDIContainerProvider _provider;

		[SetUp]
		public void Initialize()
		{
			_provider = new DryIocDIProvider();
		}

		#region Existence tests

		[Test]
		public void Resolve_NotRegistered_ContainerException()
		{
			// Act & Assert

			var ex = Assert.Throws<ContainerException>(() => _provider.Resolve<NonDepFoo>());
			Assert.That(ex.Message, Does.StartWith("code: Error.UnableToResolveUnknownService"));
			Assert.That(ex.Message, Does.Contain("Unable to resolve resolution root"));
			Assert.That(ex.Message, Does.Contain("NonDepFoo"));
		}

		[Test]
		public void ScopedResolve_NotRegistered_ContainerException()
		{
			// Act & Assert
			using var scope = _provider.BeginLifetimeScope();

			var ex = Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<NonDepFoo>());
			Assert.That(ex.Message, Does.StartWith("code: Error.UnableToResolveUnknownService"));
			Assert.That(ex.Message, Does.Contain("Unable to resolve resolution root"));
			Assert.That(ex.Message, Does.Contain("NonDepFoo"));
		}

		[Test]
		public void Resolve_ScopeRegisteredAndRequestedOutsideOfTheScope_ContainerException()
		{
			// Assign
			_provider.Register<NonDepFoo>();

			// Act & Assert

			var ex = Assert.Throws<ContainerException>(() => _provider.Resolve<NonDepFoo>());
			Assert.That(ex.Message, Does.StartWith("code: Error.NoCurrentScope"));
		}

		[Test]
		public void ScopedResolve_ScopeRegistered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>();

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void ScopedResolve_ScopeDelegateRegistered_Resolved()
		{
			// Assign

			_provider.Register(r => new NonDepFoo());

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_SingletonRegistered_Resolved()
		{
			// Assign
			_provider.Register<NonDepFoo>(LifetimeType.Singleton);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_SingletonDelegateRegistered_Resolved()
		{
			// Assign
			_provider.Register(r => new NonDepFoo(), LifetimeType.Singleton);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void ScopedResolve_SingletonRegistered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>(LifetimeType.Singleton);

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_TransientRegistered_Resolved()
		{
			// Assign
			_provider.Register<NonDepFoo>(LifetimeType.Transient);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_TransientDelegateRegistered_Resolved()
		{
			// Assign
			_provider.Register(r => new NonDepFoo(), LifetimeType.Transient);

			// Act
			var foo = _provider.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void ScopedResolve_TransientRegistered_Resolved()
		{
			// Assign

			_provider.Register<NonDepFoo>(LifetimeType.Transient);

			NonDepFoo foo;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<NonDepFoo>();

			// Assert
			Assert.IsNotNull(foo);
		}

		[Test]
		public void Resolve_InterfaceWithImplementationTypeAndTransient_Resolved()
		{
			// Assign
			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			// Act
			var bar = _provider.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void ScopedResolve_InterfaceWithImplementationTypeAndTransient_Resolved()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			IBar bar;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void Resolve_InterfaceWithImplementationTypeAndTransientAndDelegateRegistration_Resolved()
		{
			// Assign
			_provider.Register<IBar>(r => new Bar(), LifetimeType.Transient);

			// Act
			var bar = _provider.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		[Test]
		public void ScopedResolve_InterfaceWithImplementationTypeAndTransientAndDelegateRegistration_Resolved()
		{
			// Assign

			_provider.Register<IBar>(r => new Bar(), LifetimeType.Transient);

			IBar bar;

			// Act
			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			// Assert
			Assert.IsNotNull(bar);
		}

		#endregion Existence tests

		#region Reuse tests

		[Test]
		public void ScopedResolve_Scoped_ResolvedAndReusedInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>();

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				bar = scope.Resolver.Resolve<IBar>();
				barSecond = scope.Resolver.Resolve<IBar>();
			}

			// Assert

			Assert.IsNotNull(bar);
			Assert.IsNotNull(barSecond);

			Assert.AreEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_Scoped_NotReusedBetweenScope()
		{
			// Assign

			_provider.Register<IBar, Bar>();

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			using (var scope = _provider.BeginLifetimeScope())
				barSecond = scope.Resolver.Resolve<IBar>();

			// Assert

			Assert.IsNotNull(bar);
			Assert.IsNotNull(barSecond);

			Assert.AreNotEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_ScopedDependsOnScoped_ResolvedAndReusedInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			// Assert

			Assert.IsNotNull(foo.Bar);

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_ScopedDelegateDependsOnScoped_ResolvedAndReusedInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()));

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			// Assert

			Assert.IsNotNull(foo.Bar);

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_ScopedDependsOnScoped_NotReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			using (var scope = _provider.BeginLifetimeScope())
				fooSecond = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.IsNotNull(foo.Bar);

			Assert.AreNotEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_ScopedDelegateDependsOnScoped_NotReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()));

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			using (var scope = _provider.BeginLifetimeScope())
				fooSecond = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.IsNotNull(foo.Bar);

			Assert.AreNotEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_ScopedDependsOnSingleton_ReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo, Foo>();

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			using (var scope = _provider.BeginLifetimeScope())
				fooSecond = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_ScopedDelegateDependsOnSingleton_ReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()));

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			using (var scope = _provider.BeginLifetimeScope())
				fooSecond = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
		}

		// Note: this behavior check is not available
		//[Test]
		//public void ScopedResolve_ScopedDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo, Foo>();

		//	using (var scope = _provider.BeginLifetimeScope())
		//	{
		//		// Act && Assert
		//		Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
		//	}
		//}

		// Note: this behavior check is not available
		//[Test]
		//public void ScopedResolve_ScopedDelegateDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()));

		//	using (var scope = _provider.BeginLifetimeScope())
		//	{
		//		// Act && Assert
		//		Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
		//	}
		//}

		[Test]
		public void ScopedResolve_ScopedDelegateDependsOnTransient_TransientReusedAsScoped()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()));

			IFoo foo;
			IFoo fooSecond;
			IFoo fooThird;

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			using (var scope = _provider.BeginLifetimeScope())
				fooThird = scope.Resolver.Resolve<IFoo>();

			Assert.IsNotNull(foo);

			Assert.AreEqual(foo, fooSecond);
			Assert.AreNotEqual(foo, fooThird);

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
			Assert.AreNotEqual(foo.Bar, fooThird.Bar);
		}

		[Test]
		public void ScopedResolve_SingletonType_ReusedInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				bar = scope.Resolver.Resolve<IBar>();
				barSecond = scope.Resolver.Resolve<IBar>();
			}

			// Assert

			Assert.IsNotNull(bar);

			Assert.AreEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_SingletonType_ReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			using (var scope = _provider.BeginLifetimeScope())
				barSecond = scope.Resolver.Resolve<IBar>();

			// Assert

			Assert.IsNotNull(bar);

			Assert.AreEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_SingletonDependsOnScoped_ContainerException()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

			// Act && Assert

			using var scope = _provider.BeginLifetimeScope();

			var ex = Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
			Assert.That(ex.Message, Does.Contain("code: Error.DependencyHasShorterReuseLifespan"));
		}

		// Note: this behavior check is not available
		//[Test]
		//public void ScopedResolve_SingletonDelegateDependsOnScoped_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>();
		//	_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()), LifetimeType.Singleton);

		//	// Act && Assert

		//	using var scope = _provider.BeginLifetimeScope();
		//	Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
		//}

		[Test]
		public void ScopedResolve_SingletonDelegateDependsOnScoped_ScopedReusedAsSingleton()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()), LifetimeType.Singleton);

			IFoo foo;
			IFoo fooSecond;
			IFoo fooThird;

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			using (var scope = _provider.BeginLifetimeScope())
				fooThird = scope.Resolver.Resolve<IFoo>();

			Assert.IsNotNull(foo);

			Assert.AreEqual(foo, fooSecond);
			Assert.AreEqual(foo, fooThird);

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
			Assert.AreEqual(foo.Bar, fooThird.Bar);
		}

		// Note: this behavior check is not available
		//[Test]
		//public void ScopedResolve_SingletonDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

		//	// Act && Assert

		//	using var scope = _provider.BeginLifetimeScope();
		//	Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
		//}

		[Test]
		public void ScopedResolve_SingletonDependsOnTransient_TransientReusedAsSingleton()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);
			_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

			IFoo foo;
			IFoo fooSecond;
			IFoo fooThird;

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			using (var scope = _provider.BeginLifetimeScope())
				fooThird = scope.Resolver.Resolve<IFoo>();

			Assert.IsNotNull(foo);

			Assert.AreEqual(foo, fooSecond);
			Assert.AreEqual(foo, fooThird);

			Assert.AreEqual(foo.Bar, fooSecond.Bar);
			Assert.AreEqual(foo.Bar, fooThird.Bar);
		}

		// Note: this behavior check is not available
		//[Test]
		//public void ScopedResolve_SingletonDelegateDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo>(r => new Foo(r.Resolve<IBar>()), LifetimeType.Singleton);

		//	// Act && Assert
		//	using var scope = _provider.BeginLifetimeScope();
		//	Assert.Throws<ContainerException>(() => scope.Resolver.Resolve<IFoo>());
		//}

		[Test]
		public void ScopedResolve_TransientType_NotReusedInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				bar = scope.Resolver.Resolve<IBar>();
				barSecond = scope.Resolver.Resolve<IBar>();
			}

			// Assert

			Assert.IsNotNull(bar);
			Assert.IsNotNull(barSecond);

			Assert.AreNotEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_TransientType_NotReusedBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);

			IBar bar;
			IBar barSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				bar = scope.Resolver.Resolve<IBar>();

			using (var scope = _provider.BeginLifetimeScope())
				barSecond = scope.Resolver.Resolve<IBar>();

			// Assert

			Assert.IsNotNull(bar);
			Assert.IsNotNull(barSecond);

			Assert.AreNotEqual(bar, barSecond);
		}

		[Test]
		public void ScopedResolve_TransientDependsOnTransient_NoReuseInsideScope()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);
			_provider.Register<IFoo, Foo>(LifetimeType.Transient);

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
			{
				foo = scope.Resolver.Resolve<IFoo>();
				fooSecond = scope.Resolver.Resolve<IFoo>();
			}

			// Assert

			Assert.IsNotNull(foo.Bar);
			Assert.IsNotNull(fooSecond.Bar);

			Assert.AreNotEqual(foo, fooSecond);
			Assert.AreNotEqual(foo.Bar, fooSecond.Bar);
		}

		[Test]
		public void ScopedResolve_TransientDependsOnTransient_NoReuseBetweenScopes()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Transient);
			_provider.Register<IFoo, Foo>(LifetimeType.Transient);

			IFoo foo;
			IFoo fooSecond;

			// Act

			using (var scope = _provider.BeginLifetimeScope())
				foo = scope.Resolver.Resolve<IFoo>();

			using (var scope = _provider.BeginLifetimeScope())
				fooSecond = scope.Resolver.Resolve<IFoo>();

			// Assert

			Assert.IsNotNull(foo.Bar);
			Assert.IsNotNull(fooSecond.Bar);

			Assert.AreNotEqual(foo, fooSecond);
			Assert.AreNotEqual(foo.Bar, fooSecond.Bar);
		}

		#endregion Reuse tests

		#region Verification

		[Test]
		public void Verify_MissingDependencyRegistration_ContainerException()
		{
			// Assign
			_provider.Register<Foo>();

			// Act && Assert

			var ex = Assert.Throws<ContainerException>(() => _provider.Verify());
			Assert.That(ex.Message, Does.StartWith("code: Error.UnableToResolveUnknownService"));
			Assert.That(ex.Message, Does.Contain("IBar as parameter \"bar\""));
		}

		//	Note: this behavior check is not available
		//[Test]
		//public void Verify_ScopedDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo, Foo>();

		//	// Act && Assert
		//	Assert.Throws<ContainerException>(() => _provider.Verify());
		//}

		[Test]
		public void Verify_ScopedDependsOnSingleton_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo, Foo>();

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		[Test]
		public void Verify_ScopedDependsOnScoped_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>();

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		[Test]
		public void Verify_SingletonDependsOnScoped_ContainerException()
		{
			// Assign

			_provider.Register<IBar, Bar>();
			_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

			// Act && Assert

			var ex = Assert.Throws<ContainerException>(() => _provider.Verify());
			Assert.That(ex.Message, Does.Contain("code: Error.DependencyHasShorterReuseLifespan"));
		}

		// Note: this behavior check is not available
		//[Test]
		//public void Verify_SingletonDependsOnTransient_ContainerException()
		//{
		//	// Assign

		//	_provider.Register<IBar, Bar>(LifetimeType.Transient);
		//	_provider.Register<IFoo, Foo>(LifetimeType.Singleton);

		//	// Act && Assert
		//	Assert.Throws<ContainerException>(() => _provider.Verify());
		//}

		[Test]
		public void Verify_TransientDependsOnSingleton_NoExceptions()
		{
			// Assign

			_provider.Register<IBar, Bar>(LifetimeType.Singleton);
			_provider.Register<IFoo, Foo>(LifetimeType.Transient);

			// Act && Assert
			Assert.DoesNotThrow(() => _provider.Verify());
		}

		#endregion Verification
	}
}
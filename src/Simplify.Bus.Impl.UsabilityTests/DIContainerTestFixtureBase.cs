using System;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.Bus.Impl.UsabilityTests;

public class DIContainerTestFixtureBase
{
	private IDIContainerProvider? _container;

	protected IDIContainerProvider Container
	{
		get => _container ?? throw new ArgumentNullException();
		set => _container = value;
	}

	[SetUp]
	public virtual void Initialize() => _container = new DryIocDIProvider();

	[TearDown]
	public void Dispose() => _container?.Dispose();
}
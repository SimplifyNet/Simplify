using System;
using NUnit.Framework;

namespace Simplify.System.Tests
{
	[TestFixture]
	public class WeakSingletonTests
	{
		[Test]
		public void GetInstance_ExplicitTypeBuilder_CreatesInstanceWithTypeBuilder()
		{
			// Arrange

			var singleton = new WeakSingleton<string>(() => new string("Hello"));
			string str = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => str = singleton.Instance);
			Assert.NotNull(str);
			Assert.AreEqual(str, "Hello");
		}

		[Test]
		public void GetInstance_NullTypeBuilder_CreatesInstanceWithDefaultConstructor()
		{
			// Arrange

			var singleton = new WeakSingleton<object>();
			object obj = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => obj = singleton.Instance);
			Assert.NotNull(obj);
		}

#if RELEASE

		[Test]
		public void GetInstanceMultipleTimes_GcCollectBetween_CreatesDifferentInstances()
		{
			// Arrange

			var singleton = new WeakSingleton<TestClass>();
			var obj = singleton.Instance;
			obj.Value = "Some string";

			// Set strong reference to null and allow GC to collect unreferenced object
			obj = null;
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
			GC.WaitForPendingFinalizers();

			obj = singleton.Instance;

			// Act & Assert

			Assert.NotNull(obj);
			Assert.AreEqual(obj.Value, "Default");
		}

#endif

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			GC.Collect();
		}

		private class TestClass
		{
			public string Value = "Default";
		}
	}
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;

namespace Simplify.Extensions.Tests
{
	[TestFixture]
	public class ObservableCollectionExtensionsTests
	{
		#region AddRange tests

		[Test]
		public void AddRange_IEnumerable()
		{
			var collection = new ObservableCollection<string>();
			Assert.NotNull(collection, "Collection should not be null for test purposes");

			var items1 = new List<string?> { "hello", null, ", ", "world", "!" };
			var items2 = new List<string?> { "testing", "extensions", null, null };
			var itemsTotalCount = items1.Count + items2.Count;
			var itemsNotNullCount = items1.Count(x => x is not null) + items2.Count(x => x is not null);
			Assert.True(items1.Contains(null) || items2.Contains(null), "Items should contain at least one null value for test purposes");
			Assert.Greater(itemsNotNullCount, 0, "Items should contain at least one non-null value for test purposes");

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not add nulls
			Assert.DoesNotThrow(() => collection.AddRange(items1).AddRange(items2));

			Assert.Greater(itemsTotalCount, collection.Count, "Null items should not be added to the collection");
			Assert.AreEqual(itemsNotNullCount, collection.Count, "Non-null items should be added to the collection");
		}

		[Test]
		public void AddRange_Null_Reference()
		{
			var collection = null as ObservableCollection<int>;
			Assert.Null(collection, "Collection should be null for test purposes");
			// ReSharper disable once ExpressionIsAlwaysNull
			Assert.Catch(() => collection!.AddRange(3, 5, 9), "Null-collection should throw exception");

			collection = new ObservableCollection<int>();
			Assert.NotNull(collection, "Collection should not be null for test purposes");
			var items = null as List<int>;
			Assert.DoesNotThrow(() => collection.AddRange(items), "Null-items should not throw exception");
		}

		[Test]
		public void AddRange_params_class()
		{
			var collection = new ObservableCollection<string>();
			Assert.NotNull(collection, "Collection should not be null for test purposes");

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not add nulls
			Assert.DoesNotThrow(() => collection.AddRange("testing", null, "params").AddRange("with", null));
			const int itemsTotalCount = 5;
			const int itemsNotNullCount = 3;

			Assert.Greater(itemsTotalCount, collection.Count, "Null items should not be added to the collection");
			Assert.AreEqual(itemsNotNullCount, collection.Count, "Non-null items should be added to the collection");
		}

		[Test]
		public void AddRange_params_struct()
		{
			var collection = new ObservableCollection<int>();
			Assert.NotNull(collection, "Collection should not be null for test purposes");

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not throw exception when null-check on non-nullable struct
			Assert.DoesNotThrow(() => collection.AddRange(1, 2, 3).AddRange(4, 5));
			const int itemsCount = 5;

			Assert.AreEqual(itemsCount, collection.Count, "All items should be added to the collection");
		}

		#endregion AddRange tests
	}
}
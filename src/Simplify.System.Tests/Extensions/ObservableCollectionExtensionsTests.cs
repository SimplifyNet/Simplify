using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Simplify.System.Extensions;

namespace Simplify.System.Tests.Extensions
{
	[TestFixture]
	public class ObservableCollectionExtensionsTests
	{
		#region Common tests

		[Test]
		public void Null_Empty_Collections()
		{
			// Arrange

			var collection = null as ObservableCollection<object>;
			var items = null as object[];
			var indices = null as int[];
			var msg = "Null-collection should throw exception";

			// Act & Assert

			Assert.Catch(() => collection!.AddRange(items!), msg);
			Assert.Catch(() => collection!.AddRange(items.AsEnumerable()), msg);
			Assert.Catch(() => collection!.RefreshItems(items!), msg);
			Assert.Catch(() => collection!.RefreshItems(items.AsEnumerable()), msg);
			Assert.Catch(() => collection!.RefreshIndices(indices!), msg);
			Assert.Catch(() => collection!.RefreshIndices(indices.AsEnumerable()), msg);

			// Arrange

			collection = new ObservableCollection<object>();
			msg = "Null-items should not throw exception";

			// Act & Assert

			Assert.DoesNotThrow(() => collection.AddRange(items!), msg);
			Assert.DoesNotThrow(() => collection.AddRange(items.AsEnumerable()), msg);
			Assert.DoesNotThrow(() => collection.RefreshItems(items!), msg);
			Assert.DoesNotThrow(() => collection.RefreshItems(items.AsEnumerable()), msg);
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices!), msg);
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices.AsEnumerable()), msg);

			// Arrange

			items = new object[] { };
			indices = new int[] { };
			msg = "Empty items should not throw exception";

			// Act & Assert

			Assert.DoesNotThrow(() => collection.AddRange(items), msg);
			Assert.DoesNotThrow(() => collection.AddRange(items.AsEnumerable()), msg);
			Assert.DoesNotThrow(() => collection.RefreshItems(items), msg);
			Assert.DoesNotThrow(() => collection.RefreshItems(items.AsEnumerable()), msg);
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices), msg);
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices.AsEnumerable()), msg);
		}

		#endregion Common tests

		#region AddRange tests

		[Test]
		public void AddRange_IEnumerable()
		{
			// Arrange

			var collection = new ObservableCollection<string>();
			var items1 = new List<string?> { "hello", null, ", ", "world", "!" };
			var items2 = new List<string?> { "testing", "extensions", null, null };
			var itemsTotalCount = items1.Count + items2.Count;
			var itemsNotNullCount = items1.Count(x => x is not null) + items2.Count(x => x is not null);

			// Act & Assert

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not add nulls

			Assert.DoesNotThrow(() => collection.AddRange(items1).AddRange(items2));
			Assert.Greater(itemsTotalCount, collection.Count, "Null items should not be added to the collection");
			Assert.AreEqual(itemsNotNullCount, collection.Count, "Non-null items should be added to the collection");
		}

		[Test]
		public void AddRange_params_class()
		{
			// Arrange
			var collection = new ObservableCollection<string>();

			// Act & Assert

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
			// Arrange
			var collection = new ObservableCollection<int>();

			// Act & Assert
			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not throw exception when null-check on non-nullable struct

			Assert.DoesNotThrow(() => collection.AddRange(1, 2, 3).AddRange(4, 5));

			const int itemsCount = 5;

			Assert.AreEqual(itemsCount, collection.Count, "All items should be added to the collection");
		}

		#endregion AddRange tests

		#region RefreshItems/RefreshIndices tests

		[Test]
		public void RefreshItems_RefreshIndices_IEnumerable()
		{
			// Arrange

			var collection = new ObservableCollection<string>();
			var items1 = new List<string?> { "hello", null, ", ", "world", "!" };
			var items2 = new List<string?> { "hello", "extensions", null, null };
			var indices1 = new List<int> { -3, 2, 10 };
			var indices2 = new List<int> { -5, 3, 11 };

			// Act & Assert

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not refresh nulls and not existed items

			Assert.DoesNotThrow(() => collection.RefreshItems(items1).RefreshItems(items2));
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices1).RefreshIndices(indices2));

			// Arrange

			collection = new ObservableCollection<string>(items1!);
			var count = collection.Count;

			// Act & Assert

			Assert.DoesNotThrow(() => collection.RefreshItems(items2));
			Assert.DoesNotThrow(() => collection.RefreshIndices(indices2));
			Assert.AreEqual(count, collection.Count, "RefreshItems method should not change items count");
		}

		[Test]
		public void RefreshItems_RefreshIndices_params_class()
		{
			// Arrange
			var collection = new ObservableCollection<string>();

			// Act & Assert

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not refresh nulls and not existed items

			Assert.DoesNotThrow(() => collection.RefreshItems("hello", null).RefreshItems("world", null));

			// Arrange

			collection = new ObservableCollection<string>(new[] { "hello", "world" });
			var count = collection.Count;

			// Act & Assert

			Assert.DoesNotThrow(() => collection.RefreshItems("hello", null, "other"));
			Assert.DoesNotThrow(() => collection.RefreshIndices(-2, 4, 15));
			Assert.AreEqual(count, collection.Count, "RefreshItems method should not change items count");
		}

		[Test]
		public void RefreshItems_RefreshIndices_params_struct()
		{
			// Arrange
			var collection = new ObservableCollection<int>();

			// Act & Assert

			// AddRange extension method should:
			// 1. Allow fluent syntax
			// 2. Not refresh nulls and not existed items

			Assert.DoesNotThrow(() => collection.RefreshItems(5).RefreshItems(9));

			// Arrange

			collection = new ObservableCollection<int>(new[] { 5, 9 });
			var count = collection.Count;

			// Act & Assert

			Assert.DoesNotThrow(() => collection.RefreshItems(9, 7));
			Assert.DoesNotThrow(() => collection.RefreshIndices(-3, 1, 7));
			Assert.AreEqual(count, collection.Count, "RefreshItems method should not change items count");
		}

		#endregion RefreshItems/RefreshIndices tests
	}
}
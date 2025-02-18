using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Simplify.System.Extensions;

namespace Simplify.System.Tests.Extensions;

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

#pragma warning disable CS8604

		Assert.That(() => collection!.AddRange(items!), Throws.Exception, msg);
		Assert.That(() => collection!.AddRange(items.AsEnumerable()), Throws.Exception, msg);
		Assert.That(() => collection!.RefreshItems(items!), Throws.Exception, msg);
		Assert.That(() => collection!.RefreshItems(items.AsEnumerable()), Throws.Exception, msg);
		Assert.That(() => collection!.RefreshIndices(indices!), Throws.Exception, msg);
		Assert.That(() => collection!.RefreshIndices(indices.AsEnumerable()), Throws.Exception, msg);

		// Arrange

		collection = new ObservableCollection<object>();
		msg = "Null-items should not throw exception";

		// Act & Assert

		Assert.That(() => collection.AddRange(items!), Throws.Nothing, msg);
		Assert.That(() => collection.AddRange(items.AsEnumerable()), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshItems(items!), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshItems(items.AsEnumerable()), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshIndices(indices!), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshIndices(indices.AsEnumerable()), Throws.Nothing, msg);

#pragma warning restore CS8604

		// Arrange

		items = [];
		indices = [];
		msg = "Empty items should not throw exception";

		// Act & Assert

		Assert.That(() => collection.AddRange(items), Throws.Nothing, msg);
		Assert.That(() => collection.AddRange(items.AsEnumerable()), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshItems(items), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshItems(items.AsEnumerable()), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshIndices(indices), Throws.Nothing, msg);
		Assert.That(() => collection.RefreshIndices(indices.AsEnumerable()), Throws.Nothing, msg);
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

		Assert.That(() => collection.AddRange(items1).AddRange(items2), Throws.Nothing);
		Assert.That(collection.Count, Is.LessThan(itemsTotalCount), "Null items should not be added to the collection");
		Assert.That(collection.Count, Is.EqualTo(itemsNotNullCount), "Non-null items should be added to the collection");
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

		Assert.That(() => collection.AddRange("testing", null, "params").AddRange("with", null), Throws.Nothing);

		const int itemsTotalCount = 5;
		const int itemsNotNullCount = 3;

		Assert.That(collection.Count, Is.LessThan(itemsTotalCount), "Null items should not be added to the collection");
		Assert.That(collection.Count, Is.EqualTo(itemsNotNullCount), "Non-null items should be added to the collection");
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

		Assert.That(() => collection.AddRange(1, 2, 3).AddRange(4, 5), Throws.Nothing);

		const int itemsCount = 5;

		Assert.That(collection.Count, Is.EqualTo(itemsCount), "All items should be added to the collection");
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

		Assert.That(() => collection.RefreshItems(items1).RefreshItems(items2), Throws.Nothing);
		Assert.That(() => collection.RefreshIndices(indices1).RefreshIndices(indices2), Throws.Nothing);

		// Arrange

		collection = new ObservableCollection<string>(items1!);
		var count = collection.Count;

		// Act & Assert

		Assert.That(() => collection.RefreshItems(items2), Throws.Nothing);
		Assert.That(() => collection.RefreshIndices(indices2), Throws.Nothing);
		Assert.That(collection.Count, Is.EqualTo(count), "RefreshItems method should not change items count");
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

		Assert.That(() => collection.RefreshItems("hello", null).RefreshItems("world", null), Throws.Nothing);

		// Arrange

		collection = new ObservableCollection<string>(new[] { "hello", "world" });
		var count = collection.Count;

		// Act & Assert

		Assert.That(() => collection.RefreshItems("hello", null, "other"), Throws.Nothing);
		Assert.That(() => collection.RefreshIndices(-2, 4, 15), Throws.Nothing);
		Assert.That(collection.Count, Is.EqualTo(count), "RefreshItems method should not change items count");
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

		Assert.That(() => collection.RefreshItems(5).RefreshItems(9), Throws.Nothing);

		// Arrange

		collection = new ObservableCollection<int>(new[] { 5, 9 });
		var count = collection.Count;

		// Act & Assert

		Assert.That(() => collection.RefreshItems(9, 7), Throws.Nothing);
		Assert.That(() => collection.RefreshIndices(-3, 1, 7), Throws.Nothing);
		Assert.That(collection.Count, Is.EqualTo(count), "RefreshItems method should not change items count");
	}

	#endregion RefreshItems/RefreshIndices tests
}
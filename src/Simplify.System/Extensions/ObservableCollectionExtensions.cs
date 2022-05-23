using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Simplify.System.Extensions;

/// <summary>
/// Provides extensions for the ObservableCollection
/// </summary>
public static class ObservableCollectionExtensions
{
	#region AddRange

	/// <summary>
	/// Adds multiple items to the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="items">Added items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T?>? items)
	{
		if (collection is null) throw new ArgumentNullException(nameof(collection));

		var array = items as T[] ?? items?.ToArray();
		if (array is null || array.Length == 0) return collection;

		Invoke(collection, () =>
		{
			foreach (var item in array.Where(x => x is not null))
				collection.Add(item!);
		});

		return collection;
	}

	/// <summary>
	/// Adds multiple items to the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="items">Added items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, params T?[] items)
	{
		return collection.AddRange(items.AsEnumerable());
	}

	#endregion AddRange

	#region RefreshItems/RefreshIndices

	/// <summary>
	/// Refreshes multiple items by indices in the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="indices">Indices of refreshed items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> RefreshIndices<T>(this ObservableCollection<T> collection, IEnumerable<int>? indices)
	{
		if (collection is null) throw new ArgumentNullException(nameof(collection));

		var array = indices as int[] ?? indices?.ToArray();
		if (array is null || array.Length == 0) return collection;

		Invoke(collection, () =>
		{
			foreach (var index in array.Where(i => i >= 0 && i < collection.Count))
			{
				var item = collection.ElementAt(index);
				if (item is null)
					continue;
				collection.RemoveAt(index);
				collection.Insert(index, item);
			}
		});

		return collection;
	}

	/// <summary>
	/// Refreshes multiple items by indices in the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="indices">Indices of refreshed items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> RefreshIndices<T>(this ObservableCollection<T> collection, params int[] indices)
	{
		return collection.RefreshIndices(indices.AsEnumerable());
	}

	/// <summary>
	/// Refreshes multiple items in the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="items">Refreshed items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> RefreshItems<T>(this ObservableCollection<T> collection, IEnumerable<T?>? items)
	{
		if (collection is null) throw new ArgumentNullException(nameof(collection));

		var array = items as T[] ?? items?.ToArray();
		if (array is null || array.Length == 0) return collection;

		Invoke(collection, () =>
		{
			foreach (var item in array.Where(x => x is not null))
			{
				var index = collection.IndexOf(item!);
				if (index < 0 || index >= collection.Count)
					continue;
				collection.RemoveAt(index);
				collection.Insert(index, item!);
			}
		});

		return collection;
	}

	/// <summary>
	/// Refreshes multiple items in the collection
	/// </summary>
	/// <param name="collection">ObservableCollection</param>
	/// <param name="items">Refreshed items</param>
	/// <typeparam name="T">Items type</typeparam>
	/// <returns>Itself</returns>
	public static ObservableCollection<T> RefreshItems<T>(this ObservableCollection<T> collection, params T?[] items)
	{
		return collection.RefreshItems(items.AsEnumerable());
	}

	#endregion RefreshItems/RefreshIndices

	private static void Invoke(object obj, Action action)
	{
		if (obj is IConcurrentResource c)
			c.InvokeConcurrently(action);
		else
			action();
	}
}
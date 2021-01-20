using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Simplify.Extensions
{
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

			foreach (var item in array.Where(x => x is not null))
				collection.Add(item!);

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
	}
}
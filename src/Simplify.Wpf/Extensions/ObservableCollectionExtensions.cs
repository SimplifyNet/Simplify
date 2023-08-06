using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Simplify.Wpf.Extensions;

/// <summary>
/// Provides extensions for the ObservableCollection
/// </summary>
public static class ObservableCollectionExtensions
{
	/// <summary>
	/// Enables multi-thread synchronization of ObservableCollection instance
	/// </summary>
	/// <param name="collection">ObservableCollection instance</param>
	/// <param name="lockObject"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static ObservableCollection<T> EnableSynchronization<T>(this ObservableCollection<T> collection, object? lockObject = null)
	{
		if (collection is null)
			throw new ArgumentNullException(nameof(collection), "ObservableCollection instance is Null");

		BindingOperations.EnableCollectionSynchronization(collection, lockObject ?? new object());
		return collection;
	}
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Simplify.System;
using Simplify.Wpf.Extensions;

namespace Simplify.Wpf;

/// <summary>
/// Provides multi-thread synchronized ObservableCollection. Implements IConcurrentResource.
/// </summary>
/// <typeparam name="T">Data type</typeparam>
public class SynchronizedObservableCollection<T> : ObservableCollection<T>, IConcurrentResource
{
	private readonly object _locker = new();

	/// <summary>
	/// Creates instance of SynchronizedObservableCollection.
	/// </summary>
	public SynchronizedObservableCollection()
	{
		this.EnableSynchronization(_locker);
	}

	/// <summary>
	/// Creates instance of SynchronizedObservableCollection.
	/// </summary>
	/// <param name="list"></param>
	public SynchronizedObservableCollection(List<T> list) : base(list)
	{
		this.EnableSynchronization(_locker);
	}

	/// <summary>
	/// Creates instance of SynchronizedObservableCollection.
	/// </summary>
	/// <param name="collection"></param>
	public SynchronizedObservableCollection(IEnumerable<T> collection) : base(collection)
	{
		this.EnableSynchronization(_locker);
	}

	/// <summary>
	/// Invoke the action concurrently
	/// </summary>
	/// <param name="action">Action over current collection</param>
	public void InvokeConcurrently(Action action)
	{
		if (action is null) throw new ArgumentNullException(nameof(action));
		lock (_locker) action();
	}
}
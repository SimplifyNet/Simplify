using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Simplify.System;

public class ConcurrentObservableCollection<T> : ObservableCollection<T>, IConcurrentResource
{
	private readonly object _locker = new();

	public ConcurrentObservableCollection()
	{
		EnableCollectionSynchronization();
	}

	public ConcurrentObservableCollection(List<T> list) : base(list)
	{
		EnableCollectionSynchronization();
	}

	public ConcurrentObservableCollection(IEnumerable<T> collection) : base(collection)
	{
		EnableCollectionSynchronization();
	}

	public void EnableCollectionSynchronization()
	{
		BindingOperations.EnableCollectionSynchronization(this, _locker);
	}

	public void InvokeConcurrently(Action action)
	{
		if (action is null) throw new ArgumentNullException(nameof(action));
		lock (_locker) action();
	}
}
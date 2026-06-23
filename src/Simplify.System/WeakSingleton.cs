using System;

namespace Simplify.System;

/// <summary>
/// Provides Singleton implemented using WeakReference
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// Creates instance of WeakSingleton
/// </remarks>
/// <param name="typeBuilder">Builder function. If null, uses default constructor.</param>
public class WeakSingleton<T>(Func<T>? typeBuilder = null) where T : class
{
	private readonly Func<T> _typeBuilder = typeBuilder ?? (() => (T)Activator.CreateInstance(typeof(T), true)!);
	private readonly WeakReference<T> _ref = new(null!);

	private readonly object _locker = new();

	/// <summary>
	/// Gets the instance of type T
	/// </summary>
	public T Instance
	{
		get
		{
			// WeakReference&lt;T&gt; instance members are not guaranteed to be thread-safe, so both the
			// read (TryGetTarget) and the write (SetTarget) are performed under the same lock to avoid
			// a torn state when one thread reads while another re-creates the collected target.
			lock (_locker)
			{
				if (_ref.TryGetTarget(out var target))
					return target;

				target = _typeBuilder();
				_ref.SetTarget(target);
				return target;
			}
		}
	}

	/// <summary>
	/// Implicitly converts WeakSingleton to type T
	/// </summary>
	/// <param name="weak"></param>
	public static implicit operator T(WeakSingleton<T> weak) => weak.Instance;
}
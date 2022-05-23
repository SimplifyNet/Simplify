using System;

namespace Simplify.System;

/// <summary>
/// Provides Singleton implemented using WeakReference
/// </summary>
/// <typeparam name="T"></typeparam>
public class WeakSingleton<T> where T : class
{
	private readonly Func<T> _typeBuilder;
	private WeakReference<T> _ref = new(null!);

	/// <summary>
	/// Creates instance of WeakSingleton
	/// </summary>
	/// <param name="typeBuilder">Builder function. If null, uses default constructor.</param>
	public WeakSingleton(Func<T>? typeBuilder = null)
	{
		_typeBuilder = typeBuilder ?? (() => (T)Activator.CreateInstance(typeof(T), true)!);
	}

	/// <summary>
	/// Gets the instance of type T
	/// </summary>
	public T Instance
	{
		get
		{
			if (_ref.TryGetTarget(out var target))
				return target;

			target = _typeBuilder();
			_ref.SetTarget(target);
			return target;
		}
	}

	/// <summary>
	/// Implicitly converts WeakSingleton to type T
	/// </summary>
	/// <param name="weak"></param>
	public static implicit operator T(WeakSingleton<T> weak)
	{
		return weak.Instance;
	}
}
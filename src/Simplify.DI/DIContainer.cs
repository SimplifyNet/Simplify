using System;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.DI;

/// <summary>
/// IOC ambient context container to register/resolve objects for DI
/// </summary>
public class DIContainer
{
	private static readonly object Locker = new();

	private static IDIContainerProvider? _current;

	/// <summary>
	/// The IOC container
	/// </summary>
	public static IDIContainerProvider Current
	{
		get
		{
			if (_current != null)
				return _current;

			lock (Locker)
				return _current ??= new DryIocDIProvider();
		}

		set => _current = value ?? throw new ArgumentNullException(nameof(value));
	}
}
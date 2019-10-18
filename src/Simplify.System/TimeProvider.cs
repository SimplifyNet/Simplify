﻿using System;

namespace Simplify.System
{
	/// <summary>
	/// Provides Time ambient context
	/// </summary>
	public static class TimeProvider
	{
		private static ITimeProvider? _currentInstance;

		/// <summary>
		/// Gets or sets the current time provider.
		/// </summary>
		/// <value>
		/// The current time provider.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static ITimeProvider Current
		{
			get => _currentInstance ??= new SystemTimeProvider();
			set => _currentInstance = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
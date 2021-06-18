﻿using System;

namespace Simplify.EntityFramework
{
	/// <summary>
	/// Provides Simplify.FluentNHibernate related exception
	/// </summary>
	/// <seealso cref="Exception" />
	public class SimplifyEntityFrameworkException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyEntityFrameworkException"/> class.
		/// </summary>
		public SimplifyEntityFrameworkException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyEntityFrameworkException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public SimplifyEntityFrameworkException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyEntityFrameworkException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public SimplifyEntityFrameworkException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
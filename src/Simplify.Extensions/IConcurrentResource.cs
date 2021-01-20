using System;

namespace Simplify.Extensions
{
	/// <summary>
	/// Describes concurrent resource shared among threads
	/// </summary>
	public interface IConcurrentResource
	{
		/// <summary>
		/// Invokes operations on shared resource with custom lock mechanism
		/// </summary>
		/// <param name="action">Operations on shared resource</param>
		void InvokeConcurrently(Action action);
	}
}
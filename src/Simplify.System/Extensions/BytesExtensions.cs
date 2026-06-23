using System;

namespace Simplify.System.Extensions;

/// <summary>
/// Provides extensions fo the bytes[] arrays
/// </summary>
public static class BytesExtensions
{
	/// <summary>
	/// Converts bytes array to a string.
	/// </summary>
	/// <param name="bytes">The bytes array.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">bytes</exception>
	/// <exception cref="ArgumentException">The array length is odd and cannot be converted to a string without losing data.</exception>
	public static string GetString(this byte[] bytes)
	{
		if (bytes == null)
			throw new ArgumentNullException(nameof(bytes));

		// An odd-length array can't be mapped to whole UTF-16 chars; fail loudly instead of silently dropping the last byte.
		if (bytes.Length % sizeof(char) != 0)
			throw new ArgumentException("Byte array length must be even to be converted to a string.", nameof(bytes));

		var chars = new char[bytes.Length / sizeof(char)];
		Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
		return new string(chars);
	}
}
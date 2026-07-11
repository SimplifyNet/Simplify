using System;
using System.Collections.Generic;

namespace Simplify.Mail;

/// <summary>
/// Default anti-spam message pool for duplicate detection.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AntiSpamPool"/> class.
/// </remarks>
/// <param name="maxItems">Maximum number of items in the pool before oldest entries are evicted.</param>
public sealed class AntiSpamPool(int maxItems = 10000) : IAntiSpamPool
{
	private readonly Dictionary<string, DateTime> _pool = [];
	private readonly Queue<string> _order = [];
	private readonly object _lock = new();

	/// <summary>
	/// Checks if the message body was already sent within the specified lifetime and adds it if not.
	/// </summary>
	public bool CheckAndAdd(string messageBody, int lifeTimeMinutes)
	{
		lock (_lock)
		{
			if (_pool.TryGetValue(messageBody, out var addTime))
			{
				if ((DateTime.UtcNow - addTime).TotalMinutes <= lifeTimeMinutes)
					return true;

				_pool.Remove(messageBody);
			}

			PurgeExpired(lifeTimeMinutes);
			EvictIfNeeded();

			_pool.Add(messageBody, DateTime.UtcNow);
			_order.Enqueue(messageBody);

			return false;
		}
	}

	/// <summary>
	/// Clears all entries from the pool.
	/// </summary>
	public void Clear()
	{
		lock (_lock)
		{
			_pool.Clear();
			_order.Clear();
		}
	}

	private void PurgeExpired(int lifeTimeMinutes)
	{
		while (_order.Count > 0)
		{
			var oldestKey = _order.Peek();

			if (!_pool.TryGetValue(oldestKey, out var time) ||
				(DateTime.UtcNow - time).TotalMinutes <= lifeTimeMinutes)
				break;

			_order.Dequeue();
			_pool.Remove(oldestKey);
		}
	}

	private void EvictIfNeeded()
	{
		while (_pool.Count >= maxItems && _order.Count > 0)
		{
			var oldestKey = _order.Dequeue();
			_pool.Remove(oldestKey);
		}
	}
}

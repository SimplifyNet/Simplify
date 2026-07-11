using System;
using NUnit.Framework;

namespace Simplify.Mail.Tests;

[TestFixture]
public class AntiSpamPoolTests
{
	[Test]
	public void CheckAndAdd_FirstCall_ReturnsFalse()
	{
		// Arrange
		var pool = new AntiSpamPool();

		// Act
		var result = pool.CheckAndAdd("body", 10);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void CheckAndAdd_SecondCallWithinLifetime_ReturnsTrue()
	{
		// Arrange

		var pool = new AntiSpamPool(100);

		pool.CheckAndAdd("body", 10);

		// Act
		var result = pool.CheckAndAdd("body", 10);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void CheckAndAdd_DifferentBodies_NotAffected()
	{
		// Arrange

		var pool = new AntiSpamPool(100);

		pool.CheckAndAdd("body1", 10);
		pool.CheckAndAdd("body2", 10);

		// Act
		var result = pool.CheckAndAdd("body1", 10);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void CheckAndAdd_AfterClear_ReturnsFalse()
	{
		// Arrange

		var pool = new AntiSpamPool(100);

		pool.CheckAndAdd("body", 10);
		pool.Clear();

		// Act
		var result = pool.CheckAndAdd("body", 10);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void CheckAndAdd_ExceedingMaxItems_EvictsOldest()
	{
		// Arrange

		var pool = new AntiSpamPool(2);

		pool.CheckAndAdd("body1", 10);
		pool.CheckAndAdd("body2", 10);

		// Act — adding a third evicts the oldest ("body1")
		pool.CheckAndAdd("body3", 10);

		// Assert — body1 was evicted, so it should be treated as new
		Assert.That(pool.CheckAndAdd("body1", 10), Is.False);
	}

	[Test]
	public void CheckAndAdd_AfterExpiry_ReturnsFalse()
	{
		// Arrange

		var pool = new AntiSpamPool(100);

		pool.CheckAndAdd("body", 1);

		// Act — simulate passage of time by checking with lifetime 0 (effectively expired)
		var result = pool.CheckAndAdd("body", 0);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void CheckAndAdd_EmptyBody_NotBlockedByPool()
	{
		// Arrange
		var pool = new AntiSpamPool(100);

		// Act / Assert — pool doesn't filter empty bodies; MailSender does that separately
		Assert.That(pool.CheckAndAdd("", 10), Is.False);
		Assert.That(pool.CheckAndAdd("", 10), Is.True);
	}
}

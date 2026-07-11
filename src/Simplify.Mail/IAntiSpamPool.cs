namespace Simplify.Mail;

/// <summary>
/// Represents anti-spam message pool for duplicate detection.
/// </summary>
public interface IAntiSpamPool
{
	/// <summary>
	/// Checks if the message body was already sent within the specified lifetime and adds it if not.
	/// </summary>
	/// <param name="messageBody">The message body to check.</param>
	/// <param name="lifeTimeMinutes">The lifetime in minutes.</param>
	/// <returns><c>true</c> if the body was already sent and should be suppressed; otherwise <c>false</c>.</returns>
	bool CheckAndAdd(string messageBody, int lifeTimeMinutes);

	/// <summary>
	/// Clears all entries from the pool.
	/// </summary>
	void Clear();
}

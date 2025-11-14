using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Simplify.Mail.Settings;

namespace Simplify.Mail;

/// <summary>
/// Represent E-mail sending interface.
/// </summary>
public interface IMailSender : IDisposable
{
	/// <summary>
	/// MailSender settings.
	/// </summary>
	IMailSenderSettings Settings { get; }

	/// <summary>
	/// Get current SMTP client.
	/// </summary>
	/// <returns></returns>
	SmtpClient SmtpClient { get; }

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	void Send(SmtpClient client, MimeMessage mimeMessage, string bodyForAntiSpam = null);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task SendAsync(SmtpClient client, MimeMessage mimeMessage, string bodyForAntiSpam = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	void Send(MimeMessage mimeMessage, string bodyForAntiSpam = null);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task SendAsync(MimeMessage mimeMessage, string bodyForAntiSpam = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="from">From mail address.</param>
	/// <param name="to">Recipient e-mail address.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if message is processed to sent successfully
	/// </returns>
	void Send(SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null,
		params MimeEntity[] attachments);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="from">From mail address.</param>
	/// <param name="to">Recipient e-mail address.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if message is processed to sent successfully
	/// </returns>
	Task SendAsync(SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null,
		MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="from">From mail address.</param>
	/// <param name="to">Recipient e-mail address.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if message is processed to sent successfully</returns>
	void Send(string from, string to, string subject, string body, string bodyForAntiSpam = null,
		params MimeEntity[] attachments);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="from">From mail address.</param>
	/// <param name="to">Recipient e-mail address.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if message is processed to sent successfully</returns>
	Task SendAsync(string from, string to, string subject, string body, string bodyForAntiSpam = null,
		MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void Send(string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendAsync(string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses,
		string subject, string body, string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses,
		string subject, string body, string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void Send(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendAsync(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void SendSeparately(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendSeparatelyAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	void SendSeparately(string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments);

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
	Task SendSeparatelyAsync(string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default);
}

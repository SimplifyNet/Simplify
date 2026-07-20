using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Simplify.Mail.Settings;
using Simplify.Mail.Settings.Impl;

namespace Simplify.Mail;

/// <summary>
/// Thread-safe e-mail sending class.
/// </summary>
public class MailSender : IMailSender, IDisposable
{
	private readonly SemaphoreSlim _smtpLock = new(1, 1);
	private readonly Lazy<SmtpClient> _smtpClientLazy;
	private readonly IAntiSpamPool _antiSpamPool;
	private int _disposed;

	/// <summary>
	/// Gets or sets the default anti-spam pool used by new <see cref="MailSender"/> instances.
	/// </summary>
	public static IAntiSpamPool DefaultAntiSpamPool { get; set; } = new AntiSpamPool();

	/// <summary>
	/// Initializes a new instance of the <see cref="MailSender" /> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="configurationSectionName">Name of the configuration section in a configuration (for example in the appsettings.json configuration file).</param>
	/// <exception cref="ArgumentNullException">configurationSectionName</exception>
	public MailSender(IConfiguration configuration, string configurationSectionName = "MailSenderSettings")
	{
		if (string.IsNullOrEmpty(configurationSectionName))
			throw new ArgumentNullException(nameof(configurationSectionName));

		Settings = new ConfigurationBasedMailSenderSettings(configuration, configurationSectionName);
		_smtpClientLazy = new Lazy<SmtpClient>(() => new SmtpClient(), LazyThreadSafetyMode.ExecutionAndPublication);
		_antiSpamPool = DefaultAntiSpamPool;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MailSender"/> class.
	/// </summary>
	/// <param name="smtpServerAddress">The SMTP server address.</param>
	/// <param name="smtpServerPortNumber">The SMTP server port number.</param>
	/// <param name="smtpUserName">Name of the SMTP user.</param>
	/// <param name="smtpUserPassword">The SMTP user password.</param>
	/// <param name="enableSsl">if set to <c>true</c> [enable SSL].</param>
	/// <param name="antiSpamMessagesPoolOn">if set to <c>true</c> [anti spam messages pool on].</param>
	/// <param name="antiSpamPoolMessageLifeTime">The anti spam pool message life time.</param>
	/// <param name="secureSocketOptions">The secure socket options. When set, overrides <paramref name="enableSsl"/>.</param>
	/// <exception cref="ArgumentNullException">smtpServerAddress</exception>
	public MailSender(string smtpServerAddress, int smtpServerPortNumber, string smtpUserName, string smtpUserPassword,
		bool enableSsl = false, bool antiSpamMessagesPoolOn = true, int antiSpamPoolMessageLifeTime = 125,
		SecureSocketOptions? secureSocketOptions = null)
	{
		if (string.IsNullOrEmpty(smtpServerAddress)) throw new ArgumentNullException(nameof(smtpServerAddress));

		Settings = new MailSenderSettings(smtpServerAddress, smtpServerPortNumber, smtpUserName, smtpUserPassword, enableSsl,
			antiSpamMessagesPoolOn, antiSpamPoolMessageLifeTime, secureSocketOptions);
		_smtpClientLazy = new Lazy<SmtpClient>(() => new SmtpClient(), LazyThreadSafetyMode.ExecutionAndPublication);
		_antiSpamPool = DefaultAntiSpamPool;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MailSender"/> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	/// <exception cref="ArgumentNullException">settings</exception>
	public MailSender(IMailSenderSettings settings)
	{
		Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		_smtpClientLazy = new Lazy<SmtpClient>(() => new SmtpClient(), LazyThreadSafetyMode.ExecutionAndPublication);
		_antiSpamPool = DefaultAntiSpamPool;
	}

	/// <summary>
	/// MailSender settings.
	/// </summary>
	public IMailSenderSettings Settings { get; }

	/// <summary>
	/// Get current SMTP client.
	/// </summary>
	public SmtpClient SmtpClient => _smtpClientLazy.Value;

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	public void Send(SmtpClient client, MimeMessage mimeMessage, string bodyForAntiSpam = null)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? mimeMessage.HtmlBody))
			return;

		_smtpLock.Wait();

		try
		{
			Connect(client);
			client.Send(mimeMessage);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public async Task SendAsync(SmtpClient client, MimeMessage mimeMessage, string bodyForAntiSpam = null,
		CancellationToken cancellationToken = default)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? mimeMessage.HtmlBody))
			return;

		await _smtpLock.WaitAsync(cancellationToken);

		try
		{
			await ConnectAsync(client, cancellationToken);
			await client.SendAsync(mimeMessage, cancellationToken);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	public void Send(MimeMessage mimeMessage, string bodyForAntiSpam = null) => Send(_smtpClientLazy.Value, mimeMessage, bodyForAntiSpam);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="mimeMessage">The MIME message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public Task SendAsync(MimeMessage mimeMessage, string bodyForAntiSpam = null, CancellationToken cancellationToken = default) =>
		SendAsync(_smtpClientLazy.Value, mimeMessage, bodyForAntiSpam, cancellationToken);

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
	public void Send(SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null,
		params MimeEntity[] attachments)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(from, subject, body, to: to, attachments: attachments);

		_smtpLock.Wait();

		try
		{
			Connect(client);
			client.Send(message);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Sends the asynchronous.
	/// </summary>
	/// <param name="client">The client.</param>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="subject">The subject.</param>
	/// <param name="body">The body.</param>
	/// <param name="bodyForAntiSpam">The body for anti spam.</param>
	/// <param name="attachments">The attachments.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public async Task SendAsync(SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null,
		MimeEntity[] attachments = null, CancellationToken cancellationToken = default)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(from, subject, body, to: to, attachments: attachments);

		await _smtpLock.WaitAsync(cancellationToken);
		try
		{
			await ConnectAsync(client, cancellationToken);
			await client.SendAsync(message, cancellationToken);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="from">From mail address.</param>
	/// <param name="to">Recipient e-mail address.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(string from, string to, string subject, string body, string bodyForAntiSpam = null, params MimeEntity[] attachments) =>
		Send(_smtpClientLazy.Value, from, to, subject, body, bodyForAntiSpam, attachments);

	/// <summary>
	/// Sends the asynchronous.
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="subject">The subject.</param>
	/// <param name="body">The body.</param>
	/// <param name="bodyForAntiSpam">The body for anti spam.</param>
	/// <param name="attachments">The attachments.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public Task SendAsync(string from, string to, string subject, string body, string bodyForAntiSpam = null, MimeEntity[] attachments = null,
		CancellationToken cancellationToken = default) =>
		SendAsync(_smtpClientLazy.Value, from, to, subject, body, bodyForAntiSpam, attachments, cancellationToken);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, IList<string> bccAddresses = null, params MimeEntity[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(fromMailAddress, subject, body, addresses: addresses, attachments: attachments, bccAddresses: bccAddresses);

		_smtpLock.Wait();
		try
		{
			Connect(client);
			client.Send(message);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public async Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, IList<string> bccAddresses = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(fromMailAddress, subject, body, addresses: addresses, attachments: attachments, bccAddresses: bccAddresses);

		await _smtpLock.WaitAsync(cancellationToken);
		try
		{
			await ConnectAsync(client, cancellationToken);
			await client.SendAsync(message, cancellationToken);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		IList<string> bccAddresses = null, params MimeEntity[] attachments) =>
		Send(_smtpClientLazy.Value, fromMailAddress, addresses, subject, body, bodyForAntiSpam, bccAddresses, attachments);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public Task SendAsync(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		IList<string> bccAddresses = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default) =>
		SendAsync(_smtpClientLazy.Value, fromMailAddress, addresses, subject, body, bodyForAntiSpam, bccAddresses, attachments, cancellationToken);

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
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null, IList<string> bccAddresses = null, params MimeEntity[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(fromMailAddress, subject, body, addresses: addresses, ccAddresses: ccAddresses, attachments: attachments, bccAddresses: bccAddresses);

		_smtpLock.Wait();
		try
		{
			Connect(client);
			client.Send(message);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

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
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public async Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
		string body, string bodyForAntiSpam = null, IList<string> bccAddresses = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		using var message = CreateMimeMessage(fromMailAddress, subject, body, addresses: addresses, ccAddresses: ccAddresses, attachments: attachments, bccAddresses: bccAddresses);

		await _smtpLock.WaitAsync(cancellationToken);
		try
		{
			await ConnectAsync(client, cancellationToken);
			await client.SendAsync(message, cancellationToken);
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
		string body, string bodyForAntiSpam = null, IList<string> bccAddresses = null, params MimeEntity[] attachments) =>
		Send(_smtpClientLazy.Value, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, bccAddresses, attachments);

	/// <summary>
	/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="ccAddresses">Carbon copy recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="bccAddresses">BCC recipients.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	public Task SendAsync(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null, IList<string> bccAddresses = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default) =>
		SendAsync(_smtpClientLazy.Value, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, bccAddresses, attachments, cancellationToken);

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
	public void SendSeparately(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params MimeEntity[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		_smtpLock.Wait();

		try
		{
			Connect(client);

			foreach (var item in addresses)
			{
				using var message = CreateMimeMessage(fromMailAddress, subject, body, to: item, attachments: attachments);
				client.Send(message);
			}
		}
		finally
		{
			_smtpLock.Release();
		}
	}

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
	public async Task SendSeparatelyAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, MimeEntity[] attachments = null, CancellationToken cancellationToken = default)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		await _smtpLock.WaitAsync(cancellationToken);

		try
		{
			await ConnectAsync(client, cancellationToken);

			foreach (var item in addresses)
			{
				using var message = CreateMimeMessage(fromMailAddress, subject, body, to: item, attachments: attachments);
				await client.SendAsync(message, cancellationToken);
			}
		}
		finally
		{
			_smtpLock.Release();
		}
	}

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void SendSeparately(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		params MimeEntity[] attachments) =>
		SendSeparately(_smtpClientLazy.Value, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);

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
	public Task SendSeparatelyAsync(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		MimeEntity[] attachments = null, CancellationToken cancellationToken = default) =>
		SendSeparatelyAsync(_smtpClientLazy.Value, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments, cancellationToken);

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose()
	{
		if (Interlocked.Exchange(ref _disposed, 1) == 1)
			return;

		_smtpLock.Wait();
		try
		{
			if (_smtpClientLazy.IsValueCreated)
			{
				var client = _smtpClientLazy.Value;
				if (client.IsConnected)
					client.Disconnect(true);
				client.Dispose();
			}
		}
		finally
		{
			_smtpLock.Release();
			_smtpLock.Dispose();
		}

		GC.SuppressFinalize(this);
	}

	private async Task ConnectAsync(SmtpClient client, CancellationToken cancellationToken = default)
	{
		if (client.IsConnected)
			return;

		var secureSocketOptions = Settings.SecureSocketOptions ?? (Settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.StartTlsWhenAvailable);

		await client.ConnectAsync(Settings.SmtpServerAddress, Settings.SmtpServerPortNumber, secureSocketOptions, cancellationToken);

		if (!string.IsNullOrEmpty(Settings.SmtpUserName))
			await client.AuthenticateAsync(Settings.SmtpUserName, Settings.SmtpUserPassword, cancellationToken);
	}

	private void Connect(SmtpClient client)
	{
		if (client.IsConnected)
			return;

		var secureSocketOptions = Settings.SecureSocketOptions ?? (Settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.StartTlsWhenAvailable);

		client.Connect(Settings.SmtpServerAddress, Settings.SmtpServerPortNumber, secureSocketOptions);

		if (!string.IsNullOrEmpty(Settings.SmtpUserName))
			client.Authenticate(Settings.SmtpUserName, Settings.SmtpUserPassword);
	}

	private static MimeMessage CreateMimeMessage(string from, string subject, string body, string to = null,
		IList<string> addresses = null, IList<string> ccAddresses = null, IList<string> bccAddresses = null, MimeEntity[] attachments = null)
	{
		var message = new MimeMessage();
		message.From.Add(MailboxAddress.Parse(from));

		if (!string.IsNullOrEmpty(to))
			message.To.Add(MailboxAddress.Parse(to));

		if (addresses != null)
			foreach (var address in addresses)
				message.To.Add(MailboxAddress.Parse(address));

		if (ccAddresses != null)
			foreach (var ccAddress in ccAddresses)
				message.Cc.Add(MailboxAddress.Parse(ccAddress));

		if (bccAddresses != null)
			foreach (var bccAddress in bccAddresses)
				message.Bcc.Add(MailboxAddress.Parse(bccAddress));

		message.Subject = subject;

		var bodyBuilder = new BodyBuilder { HtmlBody = body };

		if (attachments != null)
			foreach (var attachment in attachments)
				bodyBuilder.Attachments.Add(attachment);

		message.Body = bodyBuilder.ToMessageBody();

		return message;
	}

	private bool CheckAntiSpamPool(string messageBody)
	{
		if (!Settings.AntiSpamMessagesPoolOn || string.IsNullOrEmpty(messageBody))
			return false;

		return _antiSpamPool.CheckAndAdd(messageBody, Settings.AntiSpamPoolMessageLifeTime);
	}
}

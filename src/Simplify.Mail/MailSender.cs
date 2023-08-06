using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Simplify.Mail.Settings;
using Simplify.Mail.Settings.Impl;

namespace Simplify.Mail;

/// <summary>
/// E-mail sending class.
/// </summary>
public class MailSender : IMailSender, IDisposable
{
	private static readonly object AntiSpamLocker = new();
	private static readonly Dictionary<string, DateTime> AntiSpamPool = new();

	private static IMailSender _defaultInstance;

	private readonly object _sendLocker = new();

	private volatile SmtpClient _smtpClient;

	/// <summary>
	/// Initializes a new instance of the <see cref="MailSender"/> class.
	/// </summary>
	/// <param name="configurationSectionName">Name of the configuration section in the *.config configuration file.</param>
	public MailSender(string configurationSectionName = "MailSenderSettings")
	{
		if (string.IsNullOrEmpty(configurationSectionName))
			throw new ArgumentNullException(nameof(configurationSectionName));

		Settings = new ConfigurationManagedBasedMailSenderSettings(configurationSectionName);
	}

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
	/// <exception cref="ArgumentNullException">smtpServerAddress</exception>
	public MailSender(string smtpServerAddress, int smtpServerPortNumber, string smtpUserName, string smtpUserPassword,
		bool enableSsl = false, bool antiSpamMessagesPoolOn = true, int antiSpamPoolMessageLifeTime = 125)
	{
		if (string.IsNullOrEmpty(smtpServerAddress)) throw new ArgumentNullException(nameof(smtpServerAddress));

		Settings = new MailSenderSettings(smtpServerAddress, smtpServerPortNumber, smtpUserName, smtpUserPassword, enableSsl,
			antiSpamMessagesPoolOn, antiSpamPoolMessageLifeTime);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MailSender"/> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	/// <exception cref="ArgumentNullException">settings</exception>
	public MailSender(IMailSenderSettings settings) => Settings = settings ?? throw new ArgumentNullException(nameof(settings));

	/// <summary>
	/// Default MailSender instance.
	/// </summary>
	/// <value>
	/// The default.
	/// </value>
	/// <exception cref="ArgumentNullException">value</exception>
	public static IMailSender Default
	{
		get => _defaultInstance ??= new MailSender();
		set => _defaultInstance = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// MailSender settings.
	/// </summary>
	public IMailSenderSettings Settings { get; }

	/// <summary>
	/// Get current SMTP client.
	/// </summary>
	public SmtpClient SmtpClient
	{
		get
		{
			if (_smtpClient != null)
				return _smtpClient;

			lock (_sendLocker)
			{
				if (_smtpClient != null)
					return _smtpClient;

				var smtpClient = new SmtpClient(Settings.SmtpServerAddress, Settings.SmtpServerPortNumber)
				{
					EnableSsl = Settings.EnableSsl
				};

				if (!string.IsNullOrEmpty(Settings.SmtpUserName))
				{
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new NetworkCredential(Settings.SmtpUserName, Settings.SmtpUserPassword);
				}

				_smtpClient = smtpClient;
			}

			return _smtpClient;
		}
	}

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mailMessage">The mail message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	public void Send(SmtpClient client, MailMessage mailMessage, string bodyForAntiSpam = null)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? mailMessage.Body))
			return;

		lock (_sendLocker)
			client.Send(mailMessage);
	}

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="client">Smtp client.</param>
	/// <param name="mailMessage">The mail message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <returns></returns>
	public Task SendAsync(SmtpClient client, MailMessage mailMessage, string bodyForAntiSpam = null) =>
		CheckAntiSpamPool(bodyForAntiSpam ?? mailMessage.Body)
			? Task.Delay(0)
			: client.SendMailAsync(mailMessage);

	/// <summary>
	/// Send single e-mail.
	/// </summary>
	/// <param name="mailMessage">The mail message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	public void Send(MailMessage mailMessage, string bodyForAntiSpam = null) => Send(SmtpClient, mailMessage, bodyForAntiSpam);

	/// <summary>
	/// Send single e-mail asynchronously.
	/// </summary>
	/// <param name="mailMessage">The mail message.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <returns></returns>
	public Task SendAsync(MailMessage mailMessage, string bodyForAntiSpam = null) => SendAsync(SmtpClient, mailMessage, bodyForAntiSpam);

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
		params Attachment[] attachments)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		var mm = new MailMessage(from, to, subject, body)
		{
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		lock (_sendLocker)
			client.Send(mm);
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
	/// <returns></returns>
	public Task SendAsync(SmtpClient client, string @from, string to, string subject, string body, string bodyForAntiSpam = null,
		params Attachment[] attachments)
	{
		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return Task.Delay(0);

		var mm = new MailMessage(from, to, subject, body)
		{
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		return client.SendMailAsync(mm);
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
	public void Send(string from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments) =>
		Send(SmtpClient, from, to, subject, body, bodyForAntiSpam, attachments);

	/// <summary>
	/// Sends the asynchronous.
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="subject">The subject.</param>
	/// <param name="body">The body.</param>
	/// <param name="bodyForAntiSpam">The body for anti spam.</param>
	/// <param name="attachments">The attachments.</param>
	/// <returns></returns>
	public Task SendAsync(string @from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments) =>
		SendAsync(SmtpClient, from, to, subject, body, bodyForAntiSpam, attachments);

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
	public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null, params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		var mm = new MailMessage
		{
			From = new MailAddress(fromMailAddress),
			Subject = subject,
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			Body = body,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		foreach (var item in addresses)
			mm.To.Add(item);

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		lock (_sendLocker)
			client.Send(mm);
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
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if all messages are processed to sent successfully
	/// </returns>
	public Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null,
		params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return Task.Delay(0);

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return Task.Delay(0);

		var mm = new MailMessage
		{
			From = new MailAddress(fromMailAddress),
			Subject = subject,
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			Body = body,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		foreach (var item in addresses)
			mm.To.Add(item);

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		return client.SendMailAsync(mm);
	}

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		params Attachment[] attachments) =>
		Send(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);

	/// <summary>
	/// Send e-mail to multiple recipients in one e-mail asynchronously.
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if all messages are processed to sent successfully
	/// </returns>
	public Task SendAsync(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		params Attachment[] attachments) =>
		SendAsync(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);

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
	public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null, params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		var mm = new MailMessage
		{
			From = new MailAddress(fromMailAddress),
			Subject = subject,
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			Body = body,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		foreach (var item in addresses)
			mm.To.Add(item);

		foreach (var item in ccAddresses)
			mm.CC.Add(item);

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		lock (_sendLocker)
			client.Send(mm);
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
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if all messages are processed to sent successfully
	/// </returns>
	public Task SendAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
		string body,
		string bodyForAntiSpam = null, params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return Task.Delay(0);

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return Task.Delay(0);

		var mm = new MailMessage
		{
			From = new MailAddress(fromMailAddress),
			Subject = subject,
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
			Body = body,
			DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
		};

		foreach (var item in addresses)
			mm.To.Add(item);

		foreach (var item in ccAddresses)
			mm.CC.Add(item);

		if (attachments != null)
			foreach (var attachment in attachments)
				mm.Attachments.Add(attachment);

		return client.SendMailAsync(mm);
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
	/// <param name="attachments">The attachments to an e-mail.</param>
	public void Send(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
		string body, string bodyForAntiSpam = null, params Attachment[] attachments) =>
		Send(SmtpClient, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, attachments);

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
	/// <returns>
	/// Process status, <see langword="true" /> if all messages are processed to sent successfully
	/// </returns>
	public Task SendAsync(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body,
		string bodyForAntiSpam = null,
		params Attachment[] attachments) =>
		SendAsync(SmtpClient, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, attachments);

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
		string bodyForAntiSpam = null, params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		foreach (var item in addresses)
		{
			var mm = new MailMessage(fromMailAddress, item, subject, body)
			{
				BodyEncoding = Encoding.UTF8,
				IsBodyHtml = true,
				DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
			};

			if (attachments != null)
				foreach (var attachment in attachments)
					mm.Attachments.Add(attachment);

			lock (_sendLocker)
				client.Send(mm);
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
	public async Task SendSeparatelyAsync(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body,
		string bodyForAntiSpam = null,
		params Attachment[] attachments)
	{
		if (addresses.Count == 0)
			return;

		if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
			return;

		foreach (var item in addresses)
		{
			var mm = new MailMessage(fromMailAddress, item, subject, body)
			{
				BodyEncoding = Encoding.UTF8,
				IsBodyHtml = true,
				DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
			};

			if (attachments != null)
				foreach (var attachment in attachments)
					mm.Attachments.Add(attachment);

			await client.SendMailAsync(mm);
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
		params Attachment[] attachments) =>
		SendSeparately(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);

	/// <summary>
	/// Send e-mail to multiple recipients separately
	/// </summary>
	/// <param name="fromMailAddress">From mail address.</param>
	/// <param name="addresses">Recipients.</param>
	/// <param name="subject">e-mail subject.</param>
	/// <param name="body">e-mail body.</param>
	/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking.</param>
	/// <param name="attachments">The attachments to an e-mail.</param>
	/// <returns>
	/// Process status, <see langword="true" /> if all messages are processed to sent successfully
	/// </returns>
	public Task SendSeparatelyAsync(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null,
		params Attachment[] attachments) =>
		SendSeparatelyAsync(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose() => _smtpClient?.Dispose();

	private bool CheckAntiSpamPool(string messageBody)
	{
		if (!Settings.AntiSpamMessagesPoolOn || string.IsNullOrEmpty(messageBody))
			return false;

		lock (AntiSpamLocker)
		{
			if (AntiSpamPool.ContainsKey(messageBody))
				return true;

			var itemsToRemove = GetItemsToRemove();

			foreach (var item in itemsToRemove)
				AntiSpamPool.Remove(item);

			if (AntiSpamPool.ContainsKey(messageBody))
				return true;

			AntiSpamPool.Add(messageBody, DateTime.Now);

			return false;
		}
	}

	private IList<string> GetItemsToRemove() =>
		(from item in AntiSpamPool
		 where (DateTime.Now - item.Value).TotalMinutes > Settings.AntiSpamPoolMessageLifeTime
		 select item.Key)
		.ToList();
}
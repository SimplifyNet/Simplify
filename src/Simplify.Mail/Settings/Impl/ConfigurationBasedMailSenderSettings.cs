using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.Mail.Settings.Impl;

/// <summary>
/// Represents MailSender settings based on IConfiguration
/// </summary>
public sealed class ConfigurationBasedMailSenderSettings : MailSenderSettings
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationBasedMailSenderSettings" /> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="configSectionName">Name of the configuration section.</param>
	/// <exception cref="MailSenderException">No MailSenderSettings in config.
	/// or
	/// MailSenderSettings SmtpServerAddress is empty or missing from config.</exception>
	public ConfigurationBasedMailSenderSettings(IConfiguration configuration,
		string configSectionName = "MailSenderSettings")
	{
		var config = configuration.GetSection(configSectionName);

		if (!config.GetChildren().Any())
			throw new MailSenderException("No MailSenderSettings found in '" + configSectionName + "' section in configuration.");

		LoadGeneralSettings(config);
		LoadExtraSettings(config);
	}

	private void LoadGeneralSettings(IConfiguration config)
	{
		SmtpServerAddress = config["SmtpServerAddress"];

		if (string.IsNullOrEmpty(SmtpServerAddress))
			throw new MailSenderException("MailSenderSettings SmtpServerAddress is empty or missing from config.");

		var smtpServerPortNumberString = config["SmtpServerPortNumber"];

		if (!string.IsNullOrEmpty(smtpServerPortNumberString) && int.TryParse(smtpServerPortNumberString, out var portNumber))
			SmtpServerPortNumber = portNumber;

		SmtpUserName = config["SmtpUserName"];
		SmtpUserPassword = config["SmtpUserPassword"];
	}

	private void LoadExtraSettings(IConfiguration config)
	{
		var antiSpamPoolMessageLifeTimeString = config["AntiSpamPoolMessageLifeTime"];

		if (!string.IsNullOrEmpty(antiSpamPoolMessageLifeTimeString) && int.TryParse(antiSpamPoolMessageLifeTimeString, out var lifeTime))
			AntiSpamPoolMessageLifeTime = lifeTime;

		var antiSpamMessagesPoolOnString = config["AntiSpamMessagesPoolOn"];

		if (!string.IsNullOrEmpty(antiSpamMessagesPoolOnString) && bool.TryParse(antiSpamMessagesPoolOnString, out var poolOn))
			AntiSpamMessagesPoolOn = poolOn;

		var enableSsl = config["EnableSsl"];

		if (!string.IsNullOrEmpty(enableSsl) && bool.TryParse(enableSsl, out var sslEnabled))
			EnableSsl = sslEnabled;
	}
}
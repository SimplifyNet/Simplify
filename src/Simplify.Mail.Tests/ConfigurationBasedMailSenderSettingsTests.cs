using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Simplify.Mail.Settings.Impl;

namespace Simplify.Mail.Tests;

[TestFixture]
public class ConfigurationBasedMailSenderSettingsTests
{
	private static IConfiguration BuildConfig(Dictionary<string, string?> data)
	{
		return new ConfigurationBuilder()
			.AddInMemoryCollection(data)
			.Build();
	}

	[Test]
	public void Constructor_ValidConfig_SetsProperties()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerAddress"] = "smtp.example.com",
			["MailSenderSettings:SmtpServerPortNumber"] = "587",
			["MailSenderSettings:SmtpUserName"] = "user",
			["MailSenderSettings:SmtpUserPassword"] = "pass",
			["MailSenderSettings:EnableSsl"] = "true",
			["MailSenderSettings:AntiSpamMessagesPoolOn"] = "false",
			["MailSenderSettings:AntiSpamPoolMessageLifeTime"] = "60"
		});

		// Act
		var settings = new ConfigurationBasedMailSenderSettings(config);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(settings.SmtpServerAddress, Is.EqualTo("smtp.example.com"));
			Assert.That(settings.SmtpServerPortNumber, Is.EqualTo(587));
			Assert.That(settings.SmtpUserName, Is.EqualTo("user"));
			Assert.That(settings.SmtpUserPassword, Is.EqualTo("pass"));
			Assert.That(settings.EnableSsl, Is.True);
			Assert.That(settings.AntiSpamMessagesPoolOn, Is.False);
			Assert.That(settings.AntiSpamPoolMessageLifeTime, Is.EqualTo(60));
		});
	}

	[Test]
	public void Constructor_MissingSection_ThrowsMailSenderException()
	{
		// Arrange
		var config = BuildConfig([]);

		// Act & Assert

		var ex = Assert.Throws<MailSenderException>(() => new ConfigurationBasedMailSenderSettings(config));

		Assert.That(ex!.Message, Does.Contain("No MailSenderSettings found"));
	}

	[Test]
	public void Constructor_MissingSmtpServerAddress_ThrowsMailSenderException()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerPortNumber"] = "25"
		});

		// Act & Assert

		var ex = Assert.Throws<MailSenderException>(() => new ConfigurationBasedMailSenderSettings(config));

		Assert.That(ex!.Message, Does.Contain("SmtpServerAddress"));
	}

	[Test]
	public void Constructor_MissingOptionalValues_UsesDefaults()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerAddress"] = "smtp.example.com"
		});

		// Act
		var settings = new ConfigurationBasedMailSenderSettings(config);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(settings.SmtpServerPortNumber, Is.EqualTo(25));
			Assert.That(settings.EnableSsl, Is.False);
			Assert.That(settings.AntiSpamMessagesPoolOn, Is.True);
			Assert.That(settings.AntiSpamPoolMessageLifeTime, Is.EqualTo(125));
		});
	}

	[Test]
	public void Constructor_InvalidPortNumber_UsesDefault()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerAddress"] = "smtp.example.com",
			["MailSenderSettings:SmtpServerPortNumber"] = "not-a-number"
		});

		// Act
		var settings = new ConfigurationBasedMailSenderSettings(config);

		// Assert
		Assert.That(settings.SmtpServerPortNumber, Is.EqualTo(25));
	}

	[Test]
	public void Constructor_InvalidBoolValues_UsesDefaults()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerAddress"] = "smtp.example.com",
			["MailSenderSettings:EnableSsl"] = "not-a-bool",
			["MailSenderSettings:AntiSpamMessagesPoolOn"] = "also-not-a-bool"
		});

		// Act
		var settings = new ConfigurationBasedMailSenderSettings(config);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(settings.EnableSsl, Is.False);
			Assert.That(settings.AntiSpamMessagesPoolOn, Is.True);
		});
	}

	[Test]
	public void Constructor_CustomSectionName_ReadsCorrectSection()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["CustomSection:SmtpServerAddress"] = "smtp.custom.com"
		});

		// Act
		var settings = new ConfigurationBasedMailSenderSettings(config, "CustomSection");

		// Assert
		Assert.That(settings.SmtpServerAddress, Is.EqualTo("smtp.custom.com"));
	}

	[Test]
	public void Constructor_EmptySmtpServerAddress_ThrowsMailSenderException()
	{
		// Arrange
		var config = BuildConfig(new Dictionary<string, string?>
		{
			["MailSenderSettings:SmtpServerAddress"] = ""
		});

		// Act & Assert
		Assert.Throws<MailSenderException>(() => new ConfigurationBasedMailSenderSettings(config));
	}
}

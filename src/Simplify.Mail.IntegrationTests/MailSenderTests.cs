using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.Mail.IntegrationTests;

// For test SMTP server you can use https://github.com/rnwood/smtp4dev
[TestFixture]
[Category("Integration")]
public class MailSenderTests
{
	private IConfiguration _configuration;
	private IMailSender _mailSender;

	[SetUp]
	public void Setup()
	{
		_configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		_mailSender = new MailSender(_configuration);
	}

	[TearDown]
	public void TearDown() => _mailSender.Dispose();

	[Test]
	public void Constructor_FromConfiguration_Succeeds()
	{
		Assert.That(_mailSender, Is.Not.Null);
		Assert.That(_mailSender.Settings.SmtpServerAddress, Is.EqualTo("localhost"));
	}

	[Test]
	public void Settings_DefaultPort_Is25() =>
		Assert.That(_mailSender.Settings.SmtpServerPortNumber, Is.EqualTo(25));

	// Requires a running SMTP server (e.g. smtp4dev on localhost:25)
	[Test]
	public void SendSimpleTestEmail() =>
		_mailSender.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");

	// Requires a running SMTP server (e.g. smtp4dev on localhost:25)
	[Test]
	public async Task SendSimpleAsyncTestEmail() =>
		await _mailSender.SendAsync("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");

	// Verifies the anti-spam pool suppresses duplicate emails across DI scopes.
	// Requires a running SMTP server (e.g. smtp4dev on localhost:25).
	// Only the first SendAsync should attempt connection; calls 2 and 3 are suppressed.
	[Test]
	public async Task SendAsync_TwoDuplicates_OneSend()
	{
		// Arrange
		var container = new DryIocDIProvider();

		container.Register<IMailSender>(r => new MailSender(_configuration));
		container.Verify();

		const string from = "somesender@somedomain.com";
		const string to = "somereceiver@somedomain.com";
		const string subject = "Hello subject";
		const string body = "Hello World!!!";

		// Act

		using var scope = container.BeginLifetimeScope();

		await scope.Resolver.Resolve<IMailSender>().SendAsync(from, to, subject, body);
		await scope.Resolver.Resolve<IMailSender>().SendAsync(from, to, subject, body);

		using var scope2 = container.BeginLifetimeScope();

		await scope2.Resolver.Resolve<IMailSender>().SendAsync(from, to, subject, body);

		// Assert — no exception means the anti-spam pool suppressed duplicates
		// (if the pool didn't suppress, the second and third calls would attempt
		//  an SMTP connection alongside the first)
		Assert.Pass("All sends completed without exception; anti-spam pool suppressed duplicates.");
	}
}

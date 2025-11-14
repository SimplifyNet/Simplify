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
	public void TearDown()
	{
		_mailSender.Dispose();
	}

	[Test]
	public void SendSimpleTestEmail() =>
		_mailSender.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");

	[Test]
	public async Task SendSimpleAsyncTestEmail() =>
		await _mailSender.SendAsync("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");

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
	}
}
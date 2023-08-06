using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.Mail.IntegrationTests;

// For test SMTP server you can use https://github.com/rnwood/smtp4dev
[TestFixture]
[Category("Integration")]
public class MailSenderTests
{
	[Test]
	public void SendSimpleTestEmail()
	{
		MailSender.Default.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");
	}

	[Test]
	public Task SendSimpleAsyncTestEmail()
	{
		return MailSender.Default.SendAsync("somesender@somedomain.com",
			"somereceiver@somedomain.com", "Hello subject", "Hello World!!!");
	}

	[Test]
	public async Task SendAsync_TwoDuplicates_OneSend()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container.Register<IMailSender>(r => new MailSender());
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
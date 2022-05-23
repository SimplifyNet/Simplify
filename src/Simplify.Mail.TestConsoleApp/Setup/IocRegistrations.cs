using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Mail.TestConsoleApp.Setup;

public static class IocRegistrations
{
	public static IDIContainerProvider RegisterAll(this IDIContainerProvider provider)
	{
		provider.Register<IConfiguration>(r => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build())

			.Register<IMailSender>(r => new MailSender(r.Resolve<IConfiguration>()));

		return provider;
	}
}
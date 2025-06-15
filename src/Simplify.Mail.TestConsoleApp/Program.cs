using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Mail;
using Simplify.Mail.TestConsoleApp.Setup;

using var provider = new DryIocDIProvider();

provider.RegisterAll()
	.Verify();

using var scope = provider.BeginLifetimeScope();
var sender = scope.Resolver.Resolve<IMailSender>();

await sender.SendAsync("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject",
	"Hello World!!!");
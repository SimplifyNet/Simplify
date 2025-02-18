using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests;

[TestFixture]
public class SessionFactoryBuilderBaseTests
{
	private IConfiguration _configuration;

	[SetUp]
	public void Initialize()
	{
		_configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", false)
			.Build();
	}

	[Test]
	public void ConnectionString_MsSql_CorrectConnectionString()
	{
		// Assign
		var builder = new TestSessionFactoryBuilder(_configuration).Build();

		// Act
		var result = builder.ConnectionString;

		// Assert
		Assert.That(result, Is.EqualTo("Data Source=localhost;Initial Catalog=foodatabase;Integrated Security=False;User ID=foouser;Password=foopassword"));
	}
}
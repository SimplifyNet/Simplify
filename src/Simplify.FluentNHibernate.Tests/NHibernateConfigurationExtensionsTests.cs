using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Simplify.FluentNHibernate.Conventions;
using Simplify.FluentNHibernate.Tests.Mappings.Accounts;

namespace Simplify.FluentNHibernate.Tests;

[TestFixture]
public class NHibernateConfigurationExtensionsTests : SessionExtensionsTestsBase
{
	private SchemaUpdate _schemaUpdate;

	[SetUp]
	public void Initialize()
	{
		var configuration = CreateConfiguration();

		Configuration config = null;
		configuration.ExposeConfiguration(c => config = c);
		configuration.BuildSessionFactory();

		config.CreateIndexesForForeignKeys();

		_schemaUpdate = new SchemaUpdate(config);
	}

	[Test]
	public void UpdateSchema_WithCreateIndexesForForeignKeys_IndexesCreated()
	{
		// Act

		using var sw = new StringWriter();
		_schemaUpdate.Execute(sw.Write, false);

		// Assert

		var result = sw.ToString();
		var matches = Regex.Matches(result, @"IDX\w+");
		var indexes = (from Match match in matches select match.Value).ToList();

		Assert.That(indexes.Count, Is.EqualTo(7));
		Assert.That(indexes[0], Is.EqualTo("IDX_FK_Employee_User"));
		Assert.That(indexes[1], Is.EqualTo("IDX_FK_UsersGroups_UserID"));
		Assert.That(indexes[2], Is.EqualTo("IDX_FK_UsersGroups_GroupID"));
		Assert.That(indexes[3], Is.EqualTo("IDX_FK_Custom_UsersPrivileges_GroupID"));
		Assert.That(indexes[4], Is.EqualTo("IDX_FK_Traveler_EmployeeID"));
		Assert.That(indexes[5], Is.EqualTo("IDX_FK_User_OrganizationID"));
		Assert.That(indexes[6], Is.EqualTo("IDX_FK_UsersPrivileges_UserID"));
	}

	private static FluentConfiguration CreateConfiguration()
	{
		return Fluently.Configure()
			.InitializeFromConfigSqLiteInMemory(true)
			.AddMappingsFromAssemblyOf<UserMap>(
				ForeignKey.EndsWith("ID"),
				ForeignKeyConstraintNameConvention.WithConstraintNameConvention());
	}
}
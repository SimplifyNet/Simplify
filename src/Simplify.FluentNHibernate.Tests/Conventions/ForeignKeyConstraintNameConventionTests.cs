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

namespace Simplify.FluentNHibernate.Tests.Conventions;

[TestFixture]
public class ForeignKeyConstraintNameConventionTests : SessionExtensionsTestsBase
{
	private SchemaUpdate _schemaUpdate;

	[SetUp]
	public void Initialize()
	{
		var configuration = CreateConfiguration();

		Configuration config = null;
		configuration.ExposeConfiguration(c => config = c);

		configuration.BuildSessionFactory();

		_schemaUpdate = new SchemaUpdate(config);
	}

	[Test]
	public void UpdateSchema_WithForeignKeyConstraintNameConvention_ConstraintNamesGeneratedByConvention()
	{
		// Act

		using var sw = new StringWriter();
		_schemaUpdate.Execute(sw.Write, false);

		// Assert

		var result = sw.ToString();
		var matches = Regex.Matches(result, @"FK\w+");
		var constraints = (from Match match in matches select match.Value).ToList();

		Assert.That(constraints.Count, Is.EqualTo(7));
		Assert.That(constraints[0], Is.EqualTo("FK_Employee_User"));
		Assert.That(constraints[1], Is.EqualTo("FK_UsersGroups_UserID"));
		Assert.That(constraints[2], Is.EqualTo("FK_UsersGroups_GroupID"));
		Assert.That(constraints[3], Is.EqualTo("FK_Custom_UsersPrivileges_GroupID"));
		Assert.That(constraints[4], Is.EqualTo("FK_Traveler_EmployeeID"));
		Assert.That(constraints[5], Is.EqualTo("FK_User_OrganizationID"));
		Assert.That(constraints[6], Is.EqualTo("FK_UsersPrivileges_UserID"));
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
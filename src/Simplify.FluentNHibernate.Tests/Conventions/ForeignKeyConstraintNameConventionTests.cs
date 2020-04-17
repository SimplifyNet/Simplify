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

namespace Simplify.FluentNHibernate.Tests.Conventions
{
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

			Assert.AreEqual(7, constraints.Count);
			Assert.AreEqual("FK_Employee_User", constraints[0]);
			Assert.AreEqual("FK_UsersGroups_UserID", constraints[1]);
			Assert.AreEqual("FK_UsersGroups_GroupID", constraints[2]);
			Assert.AreEqual("FK_Custom_UsersPrivileges_GroupID", constraints[3]);
			Assert.AreEqual("FK_Traveler_EmployeeID", constraints[4]);
			Assert.AreEqual("FK_User_OrganizationID", constraints[5]);
			Assert.AreEqual("FK_UsersPrivileges_UserID", constraints[6]);
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
}
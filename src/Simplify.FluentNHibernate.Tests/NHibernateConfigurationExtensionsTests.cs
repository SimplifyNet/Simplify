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

namespace Simplify.FluentNHibernate.Tests
{
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

			Assert.AreEqual(7, indexes.Count);
			Assert.AreEqual("IDX_FK_Employee_User", indexes[0]);
			Assert.AreEqual("IDX_FK_UsersGroups_UserID", indexes[1]);
			Assert.AreEqual("IDX_FK_UsersGroups_GroupID", indexes[2]);
			Assert.AreEqual("IDX_FK_Custom_UsersPrivileges_GroupID", indexes[3]);
			Assert.AreEqual("IDX_FK_Traveler_EmployeeID", indexes[4]);
			Assert.AreEqual("IDX_FK_User_OrganizationID", indexes[5]);
			Assert.AreEqual("IDX_FK_UsersPrivileges_UserID", indexes[6]);
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
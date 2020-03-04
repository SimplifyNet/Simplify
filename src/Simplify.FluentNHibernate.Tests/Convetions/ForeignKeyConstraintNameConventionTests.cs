using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NUnit.Framework;
using Simplify.FluentNHibernate.Convetions;
using Simplify.FluentNHibernate.Tests.Mappings.Accounts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Simplify.FluentNHibernate.Tests.Convetions
{
	[TestFixture]
	public class ForeignKeyConstraintNameConventionTests : SessionExtensionsTestsBase
	{
		private NHibernate.Tool.hbm2ddl.SchemaUpdate _schemaUpdate;

		[SetUp]
		public void Initialize()
		{
			var configuration = CreateConfiguration();

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);

			configuration.BuildSessionFactory();

			_schemaUpdate = new NHibernate.Tool.hbm2ddl.SchemaUpdate(config);
		}

		[Test]
		public void InitializeFromConfigOracleClient_CorrectConfig_NoExceptions()
		{
			// Assign
			string result;

			using (var sw = new StringWriter())
			{
				_schemaUpdate.Execute(sw.Write, false);

				result = sw.ToString();
			}

			var matches = Regex.Matches(result, @"FK\w+");

			// Act
			var constraints = (from Match match in matches select match.Value).ToList();

			// Assert
			Assert.AreEqual(5, constraints.Count);
			Assert.AreEqual("FK_UsersGroups_UserID", constraints[0]);
			Assert.AreEqual("FK_UsersGroups_GroupID", constraints[1]);
			Assert.AreEqual("FK_UsersPrivileges_GroupID", constraints[2]);
			Assert.AreEqual("FK_User_OrganizationID", constraints[3]);
			Assert.AreEqual("FK_UsersPrivileges_UserID", constraints[4]);
		}

		private static FluentConfiguration CreateConfiguration()
		{
			return Fluently.Configure()
				.InitializeFromConfigSqLiteInMemory(true)
				.AddMappingsFromAssemblyOf<UserMap>(
					PrimaryKey.Name.Is(x => "ID"),
					Table.Is(x => x.EntityType.Name + "s"),
					ForeignKey.EndsWith("ID"),
					ForeignKeyConstraintNameConvention.WithConstraintNameConvention());
		}
	}
}
using System;
using System.Data.Common;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Simplify.FluentNHibernate.Tests.Mappings.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	public class SessionExtensionsTestsBase
	{
		public void CreateDatabase(Func<ISessionFactory, DbConnection> sessionBuilder)
		{
			if (sessionBuilder == null) throw new ArgumentNullException(nameof(sessionBuilder));

			var configuration = CreateConfiguration();

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, sessionBuilder(factory), null);
		}

		private static FluentConfiguration CreateConfiguration()
		{
			return Fluently.Configure()
				.InitializeFromConfigSqLiteInMemory(true)
				.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(x => "ID"));
		}
	}
}
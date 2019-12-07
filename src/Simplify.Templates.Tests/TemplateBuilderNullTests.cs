using System;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderNullTests
	{
		private static readonly object[] _testCases =
		{
			(TestDelegate) (() => TemplateBuilder.FromFile(null)),
			(TestDelegate) (() => TemplateBuilder.FromLocalFile(null)),
			(TestDelegate) (() => TemplateBuilder.FromAssembly(null, Assembly.GetExecutingAssembly())),
			(TestDelegate) (() => TemplateBuilder.FromCurrentAssembly(null))
		};

		[TestCaseSource(nameof(_testCases))]
		public void NullPath_ArgumentNullException(TestDelegate templateBuilder)
		{
			// Act
			var ex = Assert.Throws<ArgumentException>(templateBuilder);

			// Assert
			Assert.AreEqual("Value cannot be null or empty. (Parameter 'filePath')", ex.Message);
		}
	}
}
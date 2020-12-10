using System;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateTests
	{
		private const string VariableName = "var1";

		private ITemplate _tpl;

		[SetUp]
		public void Initialize()
		{
			// Arrange
			_tpl = TemplateBuilder.FromString("{" + VariableName + "}")
				.Build();
		}

		[Test]
		public void Build_FromNullString_ArgumentNullException()
		{
			// Act
			var ex = Assert.Throws<ArgumentNullException>(() => TemplateBuilder.FromString(null));

			// Assert
			Assert.AreEqual("Value cannot be null. (Parameter 'text')", ex.Message);
		}

		[Test]
		public void Set_NullVariableName_ArgumentNullException()
		{
			// Act
			var ex = Assert.Throws<ArgumentNullException>(() => _tpl.Set(null, "test"));

			// Assert
			Assert.AreEqual("Value cannot be null. (Parameter 'variableName')", ex.Message);
		}

		[Test]
		public void Add_NullVariableName_ArgumentNullException()
		{
			// Act
			var ex = Assert.Throws<ArgumentNullException>(() => _tpl.Add(null, "test"));

			// Assert
			Assert.AreEqual("Value cannot be null. (Parameter 'variableName')", ex.Message);
		}

		[Test]
		public void Rollback_RolledAfterChange()
		{
			// Act
			_tpl.Set(VariableName, "test");

			// Assert
			Assert.AreEqual("test", _tpl.Get());

			// Act
			_tpl.RollBack();

			// Assert
			Assert.AreEqual("{var1}", _tpl.Get());
		}

		[Test]
		public void GetAndRoll_RolledAfterChangeWithValueReturn()
		{
			// Act
			_tpl.Set(VariableName, "test");

			// Act & Assert
			Assert.AreEqual("test", _tpl.GetAndRoll());

			// Assert
			Assert.AreEqual("{var1}", _tpl.Get());
		}
	}
}
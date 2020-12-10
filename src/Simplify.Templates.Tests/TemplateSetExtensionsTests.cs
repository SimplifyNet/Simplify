using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateSetExtensionsTests
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
		public void Set_NullTemplate_Empty()
		{
			// Act
			_tpl.Set(VariableName, (ITemplate)null);

			// Assert
			Assert.AreEqual("", _tpl.Get());
		}

		[Test]
		public void Set_Template_SetCorrectly()
		{
			// Act
			_tpl.Set(VariableName, TemplateBuilder.FromString("a").Build());

			// Assert
			Assert.AreEqual("a", _tpl.Get());
		}

		[Test]
		public void Set_NullObject_Empty()
		{
			// Act
			_tpl.Set(VariableName, (object)null);

			// Assert
			Assert.AreEqual("", _tpl.Get());
		}

		[Test]
		public void Set_Object_ObjectClassName()
		{
			// Act
			_tpl.Set(VariableName, new object());

			// Assert
			Assert.AreEqual("System.Object", _tpl.Get());
		}

		[Test]
		public void Set_Int_SetCorrectly()
		{
			// Act
			_tpl.Set(VariableName, 17);

			// Assert
			Assert.AreEqual("17", _tpl.Get());
		}

		[Test]
		public void Set_Long_SetCorrectly()
		{
			// Act
			_tpl.Set(VariableName, (long)16);

			// Assert
			Assert.AreEqual("16", _tpl.Get());
		}

		[Test]
		public void Set_Decimal_SetCorrectly()
		{
			// Act
			_tpl.Set(VariableName, 15.5m);

			// Assert
			Assert.AreEqual("15.5", _tpl.Get());
		}

		[Test]
		public void Set_Double_SetCorrectly()
		{
			// Act
			_tpl.Set(VariableName, 1123456789123.123);

			// Assert
			Assert.AreEqual("1123456789123.123", _tpl.Get());
		}
	}
}
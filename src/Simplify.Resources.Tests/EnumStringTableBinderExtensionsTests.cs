using NUnit.Framework;

namespace Simplify.Resources.Tests;

[TestFixture]
public class EnumStringTableBinderExtensionsTests
{
	private IResourcesStringTable _uow;

	[SetUp]
	public void Initialize()
	{
		_uow = new ResourcesStringTable(true, "ProgramResources");
	}

	[Test]
	public void GetAssociatedValue_ExistingString_Found()
	{
		// Act
		var testString = _uow.GetAssociatedValue(TestType.Value1);

		// Assert
		Assert.That(testString, Is.EqualTo("Enum value 1"));
	}

	[Test]
	public void GetAssociatedValue_NoExistingString_Null()
	{
		// Act
		var testString = _uow.GetAssociatedValue(TestType.Value2);

		// Assert
		Assert.That(testString, Is.Null);
	}

	[Test]
	public void GetKeyValuePairList_NoExistingString_Null()
	{
		// Act
		var valuesList = _uow.GetKeyValuePairList<TestType>();

		// Assert
		Assert.That(valuesList.Count, Is.EqualTo(2));
		Assert.That(valuesList[0].Key, Is.EqualTo(TestType.Value1));
		Assert.That(valuesList[0].Value, Is.EqualTo("Enum value 1"));
		Assert.That(valuesList[1].Key, Is.EqualTo(TestType.Value2));
		Assert.That(valuesList[1].Value, Is.Null);
	}
}

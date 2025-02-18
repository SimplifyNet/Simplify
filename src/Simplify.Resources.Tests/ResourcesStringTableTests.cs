using System.Reflection;
using NUnit.Framework;

namespace Simplify.Resources.Tests;

[TestFixture]
public class ResourcesStringTableTests
{
	private ResourcesStringTable _uow;

	[SetUp]
	public void Initialize()
	{
	}

	[Test]
	public void ResourcesStringTableIndexer_ExistingString_Found()
	{
		//Assign

		_uow = new ResourcesStringTable(true, "ProgramResources");

		// Act
		var testString = _uow["TestString"];

		// Assert
		Assert.That(testString, Is.EqualTo("Hello World!"));
	}

	[Test]
	public void ResourcesStringTableIndexer_CustomAssemblyExistingString_Found()
	{
		//Assign

		_uow = new ResourcesStringTable(Assembly.GetAssembly(typeof(ResourcesStringTableTests)), "ProgramResources");

		// Act
		var testString = _uow["TestString"];

		// Assert
		Assert.That(testString, Is.EqualTo("Hello World!"));
	}

	[Test]
	public void ResourcesStringTableIndexer_NoExistingString_Null()
	{
		// Act
		var testString = _uow["TestString2"];

		// Assert
		Assert.That(testString, Is.Null);
	}
}
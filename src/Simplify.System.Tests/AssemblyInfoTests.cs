using System;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.System.Tests;

[TestFixture]
public class AssemblyInfoTests
{
	[Test]
	public void AssemblyInfo_GetCurrentAssemblyInfo_InformationIsCorrect()
	{
		var assemblyInfo = new AssemblyInfo(Assembly.GetAssembly(typeof(AssemblyInfoTests)) ?? throw new InvalidOperationException());

		Assert.That(assemblyInfo.CompanyName, Is.EqualTo("Alexander Krylkov"));
		Assert.That(assemblyInfo.Copyright, Is.EqualTo("Licensed under LGPL"));
		Assert.That(assemblyInfo.Description, Is.EqualTo("Simplify.System unit tests"));
		Assert.That(assemblyInfo.ProductName, Is.EqualTo("Simplify"));
		Assert.That(assemblyInfo.Title, Is.EqualTo("Simplify.System.Tests"));
		Assert.That(assemblyInfo.Version.ToString(), Is.EqualTo("1.0.0.0"));
	}
}
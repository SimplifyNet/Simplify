﻿using System;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.System.Tests
{
	[TestFixture]
	public class AssemblyInfoTests
	{
		[Test]
		public void AssemblyInfo_GetCurrentAssemblyInfo_InformationIsCorrect()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetAssembly(typeof(AssemblyInfoTests)) ?? throw new InvalidOperationException());

			Assert.AreEqual("Alexander Krylkov", assemblyInfo.CompanyName);
			Assert.AreEqual("Licensed under LGPL", assemblyInfo.Copyright);
			Assert.AreEqual("Simplify.System unit tests", assemblyInfo.Description);
			Assert.AreEqual("Simplify", assemblyInfo.ProductName);
			Assert.AreEqual("Simplify.System.Tests", assemblyInfo.Title);
			Assert.AreEqual("1.0.0.0", assemblyInfo.Version.ToString());
		}
	}
}
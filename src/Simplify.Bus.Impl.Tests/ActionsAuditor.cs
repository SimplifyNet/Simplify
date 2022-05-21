using System;
using System.Collections.Generic;

namespace Simplify.Bus.Impl.Tests
{
	public static class ActionsAuditor
	{
		public static IList<Type> ExecutedActions { get; } = new List<Type>();
	}
}
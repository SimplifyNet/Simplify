using System;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Provides pipeline related action delegate
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="item">The item.</param>
	[Obsolete("Please use IConveyor with exceptions")]
	public delegate void PipelineAction<in T>(T item);
}
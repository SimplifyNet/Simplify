using System;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Represent pipeline stage
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Obsolete("Please use IConveyor with exceptions")]
	public interface IPipelineStage<in T>
	{
		/// <summary>
		/// Executes the stage.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>
		bool Execute(T item);
	}
}
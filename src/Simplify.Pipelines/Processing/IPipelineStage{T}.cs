namespace Simplify.Pipelines.Processing
{
	[Obsolete("Please use IConveyor with exceptions")]
	/// <summary>
	/// Represent pipeline stage
	/// </summary>
	/// <typeparam name="T"></typeparam>
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
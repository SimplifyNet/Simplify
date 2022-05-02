namespace Simplify.Pipelines.Processing
{
	[Obsolete("Please use IConveyor with exceptions")]
	/// <summary>
	/// Represent pipeline processor
	/// </summary>
	public interface IPipelineProcessor
	{
		/// <summary>
		/// Executes pipeline.
		/// </summary>
		void Execute();
	}
}
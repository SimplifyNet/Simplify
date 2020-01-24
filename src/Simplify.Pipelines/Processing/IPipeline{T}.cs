namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Represent pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPipeline<in T>
	{
		/// <summary>
		/// Process item through pipeline.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>
		bool Execute(T item);
	}
}
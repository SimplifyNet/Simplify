using System.Collections.Generic;
using System.Linq;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Provides default pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="Processing.IPipeline{T}" />
	public class Pipeline<T> : IPipeline<T>
	{
		private readonly IList<IPipelineStage<T>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="Pipeline{T}"/> class.
		/// </summary>
		/// <param name="stages">The pipeline stages.</param>
		public Pipeline(IList<IPipelineStage<T>> stages)
		{
			_stages = stages;
		}

		/// <summary>
		/// Process pipeline stages.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>

		public virtual bool Execute(T item)
		{
			return _stages.All(stage => stage.Execute(item));
		}
	}
}
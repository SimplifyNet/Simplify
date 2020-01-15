using System;
using System.Collections.Generic;

namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Provides default conveyor
	/// </summary>
	/// <typeparam name="T">Conveyor item type</typeparam>
	/// <seealso cref="IConveyor{T}" />
	public class Conveyor<T> : IConveyor<T>
	{
		private readonly IList<IConveyorStage<T>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="Conveyor{T}"/> class.
		/// </summary>
		/// <param name="stages">The stages.</param>
		/// <exception cref="ArgumentNullException">stages</exception>
		public Conveyor(IList<IConveyorStage<T>> stages)
		{
			_stages = stages ?? throw new ArgumentNullException(nameof(stages));
		}

		/// <summary>
		/// Executes the specified item thru conveyor.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Execute(T item)
		{
			foreach (var stage in _stages)
				stage.Execute(item);
		}
	}
}
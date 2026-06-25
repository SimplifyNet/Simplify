using System;

namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Provides default conveyor processor
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IConveyorExecutor{T}" />
/// <remarks>
/// Initializes a new instance of the <see cref="ConveyorExecutor{T}"/> class.
/// </remarks>
/// <param name="conveyor">The conveyor.</param>
/// <param name="itemPreparer">The conveyor item preparer.</param>
/// <exception cref="ArgumentNullException">
/// conveyor
/// or
/// itemPreparer
/// </exception>
public class ConveyorExecutor<T>(IConveyor<T> conveyor, IConveyorItemPreparer<T> itemPreparer) : IConveyorExecutor<T>
{
	private readonly IConveyor<T> _conveyor = conveyor ?? throw new ArgumentNullException(nameof(conveyor));
	private readonly IConveyorItemPreparer<T> _itemPreparer = itemPreparer ?? throw new ArgumentNullException(nameof(itemPreparer));

	/// <summary>
	/// Runs the conveyor
	/// </summary>
	/// <returns>
	/// Conveyor executing item
	/// </returns>
	public virtual T Execute()
	{
		var item = _itemPreparer.GetItem();

		_conveyor.Execute(item);

		return item;
	}
}
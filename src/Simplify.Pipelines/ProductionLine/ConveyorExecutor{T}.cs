using System;

namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Provides default conveyor processor
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IConveyorExecutor{T}" />
public class ConveyorExecutor<T> : IConveyorExecutor<T>
{
	private readonly IConveyor<T> _conveyor;
	private readonly IConveyorItemPreparer<T> _itemPreparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConveyorExecutor{T}"/> class.
	/// </summary>
	/// <param name="conveyor">The conveyor.</param>
	/// <param name="itemPreparer">The conveyor item preparer.</param>
	/// <exception cref="ArgumentNullException">
	/// conveyor
	/// or
	/// itemPreparer
	/// </exception>
	public ConveyorExecutor(IConveyor<T> conveyor, IConveyorItemPreparer<T> itemPreparer)
	{
		_conveyor = conveyor ?? throw new ArgumentNullException(nameof(conveyor));
		_itemPreparer = itemPreparer ?? throw new ArgumentNullException(nameof(itemPreparer));
	}

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
using System;
using System.Threading.Tasks;

namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Provides default asynchronous conveyor processor
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IAsyncConveyorExecutor{T}" />
/// <remarks>
/// Initializes a new instance of the <see cref="AsyncConveyorExecutor{T}"/> class.
/// </remarks>
/// <param name="conveyor">The conveyor.</param>
/// <param name="itemPreparer">The conveyor item preparer.</param>
/// <exception cref="ArgumentNullException">
/// conveyor
/// or
/// itemPreparer
/// </exception>
public class AsyncConveyorExecutor<T>(IAsyncConveyor<T> conveyor, IAsyncConveyorItemPreparer<T> itemPreparer) : IAsyncConveyorExecutor<T>
{
	private readonly IAsyncConveyor<T> _conveyor = conveyor ?? throw new ArgumentNullException(nameof(conveyor));
	private readonly IAsyncConveyorItemPreparer<T> _itemPreparer = itemPreparer ?? throw new ArgumentNullException(nameof(itemPreparer));

	/// <summary>
	/// Runs the conveyor
	/// </summary>
	/// <returns>
	/// Conveyor executing item
	/// </returns>
	public virtual async Task<T> Execute()
	{
		var item = await _itemPreparer.GetItem();

		await _conveyor.Execute(item);

		return item;
	}
}
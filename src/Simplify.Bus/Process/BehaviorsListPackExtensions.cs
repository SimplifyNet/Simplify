using System.Collections.Generic;

namespace Simplify.Bus.Process;

public static class BehaviorsListPackExtensions
{
	public static BehaviorChainWrapper<TRequest>? WrapUp<TRequest>(this IReadOnlyList<IBehavior<TRequest>>? behaviors, IRequestHandler<TRequest> requestHandler)
	{
		if (behaviors == null || behaviors.Count == 0)
			return null;

		BehaviorChainWrapper<TRequest>? currentBehavior = null;

		for (var i = behaviors.Count; i > 0; i--)
			currentBehavior = new BehaviorChainWrapper<TRequest>(behaviors[i - 1], i == behaviors.Count ? null : currentBehavior, requestHandler);

		return currentBehavior!;
	}

	public static BehaviorChainWrapper<TRequest, TResponse>? WrapUp<TRequest, TResponse>(this IReadOnlyList<IBehavior<TRequest, TResponse>>? behaviors, IRequestHandler<TRequest, TResponse> requestHandler)
	{
		if (behaviors == null || behaviors.Count == 0)
			return null;

		BehaviorChainWrapper<TRequest, TResponse>? currentBehavior = null;

		for (var i = behaviors.Count; i > 0; i--)
			currentBehavior = new BehaviorChainWrapper<TRequest, TResponse>(behaviors[i - 1], i == behaviors.Count ? null : currentBehavior, requestHandler);

		return currentBehavior!;
	}
}
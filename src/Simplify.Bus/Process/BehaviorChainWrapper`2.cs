using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Process;

public class BehaviorChainWrapper<TRequest, TResponse>
{
	private readonly IBehavior<TRequest, TResponse> _behavior;
	private readonly BehaviorChainWrapper<TRequest, TResponse>? _nextBehavior;
	private readonly IRequestHandler<TRequest, TResponse>? _requestHandler;

	public BehaviorChainWrapper(IBehavior<TRequest, TResponse> behavior, BehaviorChainWrapper<TRequest, TResponse>? nextBehavior = null, IRequestHandler<TRequest, TResponse>? requestHandler = null)
	{
		_behavior = behavior;
		_nextBehavior = nextBehavior;
		_requestHandler = requestHandler;

		if (_nextBehavior == null && _requestHandler == null)
			throw new InvalidOperationException();
	}

	public Task<TResponse> Handle(TRequest request) =>
		_behavior.Handle(request, () =>
			 _nextBehavior != null
				? _nextBehavior.Handle(request)
				: _requestHandler!.Handle(request)
		);
}
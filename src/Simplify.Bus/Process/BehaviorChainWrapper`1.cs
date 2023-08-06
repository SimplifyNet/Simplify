using System;
using System.Threading.Tasks;

namespace Simplify.Bus.Process;

public class BehaviorChainWrapper<TRequest>
{
	private readonly IBehavior<TRequest> _behavior;
	private readonly BehaviorChainWrapper<TRequest>? _nextBehavior;
	private readonly IRequestHandler<TRequest>? _requestHandler;

	public BehaviorChainWrapper(IBehavior<TRequest> behavior, BehaviorChainWrapper<TRequest>? nextBehavior = null, IRequestHandler<TRequest>? requestHandler = null)
	{
		_behavior = behavior;
		_nextBehavior = nextBehavior;
		_requestHandler = requestHandler;

		if (_nextBehavior == null && _requestHandler == null)
			throw new InvalidOperationException();
	}

	public Task Handle(TRequest request) =>
		_behavior.Handle(request, () =>
			 _nextBehavior != null
				? _nextBehavior.Handle(request)
				: _requestHandler!.Handle(request)
		);
}
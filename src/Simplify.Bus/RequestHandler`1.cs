using System.Threading.Tasks;

namespace Simplify.Bus;

public delegate Task<TResponse> RequestHandler<TResponse>();
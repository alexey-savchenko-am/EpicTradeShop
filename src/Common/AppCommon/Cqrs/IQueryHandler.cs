using MediatR;
using SharedKernel.Output;

namespace AppCommon.Cqrs;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }


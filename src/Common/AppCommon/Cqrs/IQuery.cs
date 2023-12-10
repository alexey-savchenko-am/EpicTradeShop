using MediatR;
using SharedKernel.Output;

namespace AppCommon.Cqrs;

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>
{}

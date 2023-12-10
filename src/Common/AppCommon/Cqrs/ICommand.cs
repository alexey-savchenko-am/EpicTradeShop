using MediatR;
using SharedKernel.Output;

namespace AppCommon.Cqrs;

public interface ICommand
    : IRequest<Result>
{}

public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{}

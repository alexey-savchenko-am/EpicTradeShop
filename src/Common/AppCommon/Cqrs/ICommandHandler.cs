using MediatR;
using SharedKernel.Output;

namespace AppCommon.Cqrs;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result> 
    where TCommand : ICommand
{}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{}


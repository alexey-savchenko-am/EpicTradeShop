
using AppCommon.Cqrs;

namespace Presentation;

public abstract class Request<TCommand>
    where TCommand : ICommand
{

}

public abstract class Request<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    protected TCommand Command { get; }

    public Request()
    {

    }

    protected virtual bool Validate()
    {
        return true;
    }
}

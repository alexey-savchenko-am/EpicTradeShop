
using SharedKernel.Output;

namespace AppCommon.Errors;

public sealed class ValidationError
    : Error
{
    public ValidationError(string code, string message)
        : base(code, message)
    {
    }
}

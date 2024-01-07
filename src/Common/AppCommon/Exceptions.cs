using AppCommon.Errors;
using SharedKernel.Output;

namespace AppCommon;

public static class Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(List<ValidationError> errors)
        {
            Errors = errors;
        }

        public List<ValidationError> Errors { get; }
    }
}

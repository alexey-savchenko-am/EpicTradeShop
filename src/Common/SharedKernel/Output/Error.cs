namespace SharedKernel.Output;

public sealed class Error
{
    public string Code { get; }
	public string Message { get; }
	public Error? InnerError { get; }

	public Error(string code, string message)
	{
		this.Code = code;
		this.Message = message;
	}

	public Error(string code, string message, Error innerError) 
		: this(code, message)
    {
        this.InnerError = innerError;
    }

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    public static implicit operator string(Error error) => error.Code;
    public static implicit operator Result(Error error) => Result.Failure(error);
}

using System.Net;
using System.Text.Json.Serialization;

namespace SharedKernel.Output;

public class Error
{
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Code { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Message { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? InnerError { get; }


    protected Error()
    {}

    public Error(string code, string message)
	{
		Code = code;
		Message = message;
	}

	public Error(string code, string message, Error innerError) 
		: this(code, message)
    {
        InnerError = innerError;
    }

    public static readonly Error None = new();
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    public static implicit operator string?(Error error) => error.Code;
    public static implicit operator Result(Error error) => Result.Failure(error);
}

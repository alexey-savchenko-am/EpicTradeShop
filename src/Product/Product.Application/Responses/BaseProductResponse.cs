using Product.Application.JsonConverters;
using System.Text.Json.Serialization;

namespace Product.Application.Responses;


[JsonConverter(typeof(TypedProductJsonConverter))]
public abstract class BaseProductResponse
{
}

using Product.Application.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Product.Application.JsonConverters;

internal class TypedProductJsonConverter
    : JsonConverter<ProductResponse>
{
    public override ProductResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("not used");
    }

    public override void Write(Utf8JsonWriter writer, ProductResponse value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(value, value.GetType(), options);
        writer.WriteRawValue(json);
    }
}

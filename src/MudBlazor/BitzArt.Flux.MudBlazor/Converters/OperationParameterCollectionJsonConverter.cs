using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazor;

internal class OperationParameterCollectionJsonConverter : JsonConverter<IOperationParameterCollection>
{
    public override IOperationParameterCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Func<IOperationParameterCollection?> result = () => null;

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        while(reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return result.Invoke();
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
        }

        
    }

    public override void Write(Utf8JsonWriter writer, IOperationParameterCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        switch(value)
        {
            case INamedOperationParameterCollection named:
                WriteNamed(writer, named, options);
                break;
            default:
                WriteSimple(writer, value, options);
                break;
        }

        writer.WriteEndObject();
    }

    private void WriteNamed(Utf8JsonWriter writer, INamedOperationParameterCollection value, JsonSerializerOptions options)
    {
        writer.WriteString("type", "named");

        var typed = new Dictionary<string, TypedValue>(value.Values
            .Select(x => new KeyValuePair<string, TypedValue>(x.Key, TypedValue.From(x.Value))));

        writer.WriteStartArray("values");

        var converter = (JsonConverter<Dictionary<string, TypedValue>>)options.GetConverter(typeof(Dictionary<string, TypedValue>));
        converter.Write(writer, typed, options);

        writer.WriteEndArray();
    }

    private void WriteSimple(Utf8JsonWriter writer, IOperationParameterCollection value, JsonSerializerOptions options)
    {
        writer.WriteString("type", "simple");

        var typed = value.Values.Select(x => TypedValue.From(x)).ToList();

        writer.WriteStartArray("values");

        var converter = (JsonConverter<TypedValue<object>>)options.GetConverter(typeof(TypedValue));

        foreach (var item in typed)
        {
            converter.Write(writer, item, options);
        }

        writer.WriteEndArray();
    }
}

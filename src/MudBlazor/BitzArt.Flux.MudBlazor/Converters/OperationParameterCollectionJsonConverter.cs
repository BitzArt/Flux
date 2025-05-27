using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazor;

internal class OperationParameterCollectionJsonConverter : JsonConverter<IOperationParameterCollection>
{
    public override IOperationParameterCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = (JsonConverter<ParameterCollectionPayload>)options.GetConverter(typeof(ParameterCollectionPayload));
        ParameterCollectionPayload? payload = converter.Read(ref reader, typeof(ParameterCollectionPayload), options);

        if (payload is null)
        {
            return null;
        }

        return payload switch
        {
            NamedParameterCollectionPayload named => named,
            SimpleParameterCollectionPayload simple => simple,
            _ => throw new UnreachableException()
        };
    }

    public override void Write(Utf8JsonWriter writer, IOperationParameterCollection value, JsonSerializerOptions options)
    {
        ParameterCollectionPayload payload = value switch
        {
            INamedOperationParameterCollection named => new NamedParameterCollectionPayload
            {
                Values = named.Values.Select(x => new KeyValuePair<string, TypedValue<object>>(x.Key, TypedValue.From(x.Value)))
            },
            _ => new SimpleParameterCollectionPayload
            {
                Values = value.Values.Select(TypedValue.From)
            }
        };

        var converter = (JsonConverter<ParameterCollectionPayload>)options.GetConverter(typeof(ParameterCollectionPayload));
        converter.Write(writer, payload, options);
    }

    private class NamedParameterCollectionPayload : ParameterCollectionPayload, INamedOperationParameterCollection
    {
        [JsonPropertyName("values")]
        public IEnumerable<KeyValuePair<string, TypedValue<object>>> Values { get; set; }

        IEnumerable<KeyValuePair<string, object>> INamedOperationParameterCollection.Values
            => [.. Values.Select(x => new KeyValuePair<string, object>(x.Key, x.Value.Value!))];

        public NamedParameterCollectionPayload(IEnumerable<KeyValuePair<string, TypedValue<object>>> values)
        {
            Values = values;
        }

        public NamedParameterCollectionPayload()
        {
            Values = [];
        }
    }

    private class SimpleParameterCollectionPayload : ParameterCollectionPayload, IOperationParameterCollection
    {
        [JsonPropertyName("values")]
        public IEnumerable<TypedValue> Values { get; set; }

        IEnumerable<object> IOperationParameterCollection.Values => [.. Values.Select(x => x.Value!)];

        public SimpleParameterCollectionPayload(IEnumerable<TypedValue> values)
        {
            Values = values;
        }

        public SimpleParameterCollectionPayload()
        {
            Values = [];
        }
    }

    [JsonDerivedType(typeof(NamedParameterCollectionPayload), typeDiscriminator: "named")]
    [JsonDerivedType(typeof(SimpleParameterCollectionPayload), typeDiscriminator: "simple")]
    private abstract class ParameterCollectionPayload { }
}

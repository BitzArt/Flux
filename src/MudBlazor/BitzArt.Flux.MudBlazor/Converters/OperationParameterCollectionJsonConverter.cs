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

        return payload.GetCollection();
    }

    public override void Write(Utf8JsonWriter writer, IOperationParameterCollection value, JsonSerializerOptions options)
    {
        ParameterCollectionPayload payload = value switch
        {
            OperationParameterCollection.SimpleParameters simpleParameters => new SimpleParameterCollectionPayload(simpleParameters),
            OperationParameterCollection.NamedParameters namedParameters => new NamedParameterCollectionPayload(namedParameters),
            _ => new CustomParameterCollectionPayload(value)
        };

        var converter = (JsonConverter<ParameterCollectionPayload>)options.GetConverter(typeof(ParameterCollectionPayload));
        converter.Write(writer, payload, options);
    }

    private sealed class SimpleParameterCollectionPayload : ParameterCollectionPayload
    {
        [JsonPropertyName("values")]
        public IEnumerable<TypedValue> Values { get; set; }

        public override IOperationParameterCollection GetCollection()
            => new OperationParameterCollection.SimpleParameters([.. Values.Select(x => x.Value!)]);

        public SimpleParameterCollectionPayload(OperationParameterCollection.SimpleParameters collection)
        {
            Values = collection.Values.Select(x => TypedValue.From(x));
        }
        public SimpleParameterCollectionPayload()
        {
            Values = null!;
        }
    }

    private sealed class NamedParameterCollectionPayload : ParameterCollectionPayload
    {
        [JsonPropertyName("values")]
        public IEnumerable<KeyValuePair<string, TypedValue>> Values { get; set; }

        public override IOperationParameterCollection GetCollection()
            => new OperationParameterCollection.NamedParameters([.. Values.Select(x => new KeyValuePair<string, object>(x.Key, x.Value.Value!))]);

        public NamedParameterCollectionPayload(OperationParameterCollection.NamedParameters collection)
        {
            Values = collection.Values.Select(x => new KeyValuePair<string, TypedValue>(x.Key, TypedValue.From(x.Value)));
        }

        public NamedParameterCollectionPayload()
        {
            Values = null!;
        }
    }

    private sealed class CustomParameterCollectionPayload : ParameterCollectionPayload
    {
        [JsonPropertyName("collection")]
        [JsonConverter(typeof(TypedValueJsonConverter))]
        public IOperationParameterCollection Collection { get; set; }

        public override IOperationParameterCollection GetCollection() => Collection;

        public CustomParameterCollectionPayload(IOperationParameterCollection collection)
        {
            Collection = collection;
        }

        public CustomParameterCollectionPayload()
        {
            Collection = null!;
        }
    }

    [JsonDerivedType(typeof(SimpleParameterCollectionPayload), typeDiscriminator: "simple")]
    [JsonDerivedType(typeof(NamedParameterCollectionPayload), typeDiscriminator: "named")]
    [JsonDerivedType(typeof(CustomParameterCollectionPayload), typeDiscriminator: "custom")]
    private abstract class ParameterCollectionPayload
    {
        public abstract IOperationParameterCollection GetCollection();
    }
}

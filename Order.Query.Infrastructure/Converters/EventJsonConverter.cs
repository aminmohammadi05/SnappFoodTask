using CQRS.Core.Events;
using Order.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }

        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
            }

            if (!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();

            return typeDiscriminator switch
            {
                nameof(OrderCreatedEvent) => JsonSerializer.Deserialize<OrderCreatedEvent>(json, options),
                nameof(OrderUpdatedEvent) => JsonSerializer.Deserialize<OrderUpdatedEvent>(json, options),
                nameof(OrderProductAddedEvent) => JsonSerializer.Deserialize<OrderProductAddedEvent>(json, options),
                nameof(OrderProductCountChangedEvent) => JsonSerializer.Deserialize<OrderProductCountChangedEvent>(json, options),
                nameof(OrderProductRemovedEvent) => JsonSerializer.Deserialize<OrderProductRemovedEvent>(json, options),
                nameof(OrderRemovedEvent) => JsonSerializer.Deserialize<OrderRemovedEvent>(json, options),
                _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

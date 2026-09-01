using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace baseBack.API.Shared
{
    public class DataBrJsonConverter : JsonConverter<DateTime>
    {
        private const string FormatoData = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            if (DateTime.TryParseExact(stringValue, FormatoData, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            // Tenta fallback para formato ISO padrão caso seja enviado no formato tradicional
            if (DateTime.TryParse(stringValue, out var dateIso))
            {
                return dateIso;
            }

            throw new JsonException($"A data precisa estar no formato {FormatoData}. Valor recebido: '{stringValue}'");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(FormatoData, CultureInfo.InvariantCulture));
        }
    }
}

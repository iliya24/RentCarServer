using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ServerRentCar.Common.Converters
{
    class JsonDateConverter : JsonConverter<DateTime>
    {
         

        public override DateTime ReadJson(JsonReader reader, Type objectType, [AllowNull] DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }

        

        public override void WriteJson(JsonWriter writer, [AllowNull] DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("yyyy"));
        }
    }
}
 
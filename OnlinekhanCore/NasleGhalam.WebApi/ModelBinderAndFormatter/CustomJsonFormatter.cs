using System;
using NasleGhalam.Common;
using Newtonsoft.Json;
using StructureMap.TypeRules;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class CustomJsonFormatter : JsonConverter
    {
        public override bool CanWrite => false;
       
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader?.Value == null || reader.TokenType == JsonToken.Null)
                return null;

            string val = reader.Value.ToString();
            if (objectType.Name == "DateTime")
            {
                return val.ToMiladiDateTime();
            }

            if (objectType.IsString())
            {
                return val
                    .Replace("ي", "ی").Replace("ك", "ک")
                    .Trim();
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string) || objectType == typeof(DateTime);
        }
    }
}
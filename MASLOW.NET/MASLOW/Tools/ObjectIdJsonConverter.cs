using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MASLOW.Tools
{
    public class ObjectIdJsonConverter : JsonConverter<ObjectId>
    {
        public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => new ObjectId(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }
}

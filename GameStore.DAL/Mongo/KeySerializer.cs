using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GameStore.DAL.Mongo
{
    public class KeySerializer : SerializerBase<string>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            var productId = Convert.ToInt32(value);

            context.Writer.WriteInt32(productId);
        }

        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return context.Reader.ReadInt32().ToString();
        }
    }
}

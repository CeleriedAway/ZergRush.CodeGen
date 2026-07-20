using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.Samples {

    public partial struct TaggedStruct : IUpdatableFrom<ZergRush.Samples.TaggedStruct>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Samples.TaggedStruct>
    {
        public void UpdateFrom(ZergRush.Samples.TaggedStruct other, ZRUpdateFromHelper __helper) 
        {
            id = other.id;
            label = other.label;
        }
        public void Deserialize(ZRBinaryReader reader) 
        {
            id = reader.ReadInt32();
            if (!reader.ReadBoolean()) {
                label = null;
            }
            else { 
                label = reader.ReadString();
            }
        }
        public void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(id);
            if (!(label != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(label);
            }
        }
        public ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1209296537;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)id;
            hash += hash << 11; hash ^= hash >> 7;
            hash += label != null ? CodeGenImplTools.CalculateStringHash(label) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public void CompareCheck(ZergRush.Samples.TaggedStruct other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (id != other.id) CodeGenImplTools.LogCompError(__helper, "id", printer, other.id, id);
            if (label != other.label) CodeGenImplTools.LogCompError(__helper, "label", printer, other.label, label);
        }
        public bool ReadFromJson(ZRJsonTextReader reader) 
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var __name = (string) reader.Value;
                    reader.Read();
                    switch(__name)
                    {
                        case "id":
                        id = (int)(Int64)reader.Value;
                        break;
                        case "label":
                        if (reader.TokenType == JsonToken.Null) {
                            label = null;
                        }
                        else { 
                            label = (string) reader.Value;
                        }
                        break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject) { break; }
            }
            return true;
        }
        public void WriteJson(ZRJsonTextWriter writer) 
        {
            writer.WriteStartObject();
            writer.WritePropertyName("id");
            writer.WriteValue(id);
            if (!(label != null))
            {
                writer.WritePropertyName("label");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("label");
                writer.WriteValue(label);
            }
            writer.WriteEndObject();
        }
    }
}
#endif

using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class ConfigStorageCoverageItem : IBinaryDeserializable, IBinarySerializable, IHashable, IUniquelyIdentifiable, IJsonSerializable
    {
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            configId = reader.ReadInt32();
            payload = reader.ReadInt32();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(configId);
            writer.Write(payload);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)864993101;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)configId;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)payload;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override ulong UId() 
        {
            var hash = base.UId();
            hash += (ulong)configId;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override void CollectConfigs(ConfigRegister _collection) 
        {
            base.CollectConfigs(_collection);

        }
        public  ConfigStorageCoverageItem() 
        {

        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "configId":
                configId = (int)(Int64)reader.Value;
                break;
                case "payload":
                payload = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("configId");
            writer.WriteValue(configId);
            writer.WritePropertyName("payload");
            writer.WriteValue(payload);
        }
    }
}
#endif

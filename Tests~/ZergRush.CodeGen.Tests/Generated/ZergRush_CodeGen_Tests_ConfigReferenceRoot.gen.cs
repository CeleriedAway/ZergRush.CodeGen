// ZergRush serialization schema: 2 (logical collections, replacement reads, strict JSON)
using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class ConfigReferenceRoot : IBinaryDeserializable, IBinarySerializable, IHashable, IJsonSerializable
    {
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);

        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);

        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)80617491;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override void CollectConfigs(ConfigRegister _collection) 
        {
            base.CollectConfigs(_collection);

        }
        public  ConfigReferenceRoot() 
        {

        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);

        }
    }
}
#endif

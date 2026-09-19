using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class ExternalVectorHolder : IUpdatableFrom<ZergRush.CodeGen.Tests.ExternalVectorHolder>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.ExternalVectorHolder>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.ExternalVectorHolder other, ZRUpdateFromHelper __helper) 
        {
            position.UpdateFrom(other.position, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            position = reader.ReadSystem_Numerics_Vector3();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            position.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1379578861;
            hash += hash << 11; hash ^= hash >> 7;
            hash += position.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  ExternalVectorHolder() 
        {

        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.ExternalVectorHolder other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("position");
            position.CompareCheck(other.position, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "position":
                position = (System.Numerics.Vector3)reader.ReadFromJsonSystem_Numerics_Vector3();
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("position");
            position.WriteJson(writer);
        }
    }
}
#endif

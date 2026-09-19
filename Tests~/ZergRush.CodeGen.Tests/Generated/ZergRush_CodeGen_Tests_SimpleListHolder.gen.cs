using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class SimpleListHolder : IUpdatableFrom<ZergRush.CodeGen.Tests.SimpleListHolder>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.SimpleListHolder>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.SimpleListHolder other, ZRUpdateFromHelper __helper) 
        {
            values.UpdateFrom(other.values, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            values.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            values.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)337256860;
            hash += hash << 11; hash ^= hash >> 7;
            hash += values.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  SimpleListHolder() 
        {
            values = new SimpleList<int>();
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.SimpleListHolder other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("values");
            values.CompareCheck(other.values, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "values":
                values.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("values");
            values.WriteJson(writer);
        }
    }
}
#endif

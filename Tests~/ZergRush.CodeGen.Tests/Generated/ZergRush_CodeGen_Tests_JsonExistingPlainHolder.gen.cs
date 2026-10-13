// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class JsonExistingPlainHolder : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonExistingPlainHolder>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonExistingPlainHolder>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonExistingPlainHolder other, ZRUpdateFromHelper __helper) 
        {
            var __cell_value = cell.value;
            __cell_value.UpdateFrom(other.cell.value, __helper);
            cell.value = __cell_value;
            if (other.optional == null) {
                optional = null;
            }
            else { 
                if (optional == null) {
                    optional = new ZergRush.CodeGen.Tests.JsonExistingPlain();
                }
                optional.UpdateFrom(other.optional, __helper);
            }
            required.UpdateFrom(other.required, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            cell.value.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                optional = null;
            }
            else { 
                if (optional == null) {
                    optional = new ZergRush.CodeGen.Tests.JsonExistingPlain();
                }
                optional.Deserialize(reader);
            }
            required.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            cell.value.Serialize(writer);
            if (!(optional != null)) writer.Write(false);
            else {
                writer.Write(true);
                optional.Serialize(writer);
            }
            required.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)857397760;
            hash += hash << 11; hash ^= hash >> 7;
            hash += cell.value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += optional != null ? optional.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += required.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  JsonExistingPlainHolder() 
        {
            cell = new ZergRush.ReactiveCore.Cell<ZergRush.CodeGen.Tests.JsonExistingPlain>();
            cell.value = new ZergRush.CodeGen.Tests.JsonExistingPlain();
            required = new ZergRush.CodeGen.Tests.JsonExistingPlain();
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonExistingPlainHolder other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("cell");
            cell.value.CompareCheck(other.cell.value, __helper, printer);
            __helper.Pop();
            if (CodeGenImplTools.CompareNull(__helper, "optional", printer, optional, other.optional)) {
                __helper.Push("optional");
                optional.CompareCheck(other.optional, __helper, printer);
                __helper.Pop();
            }
            __helper.Push("required");
            required.CompareCheck(other.required, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "cell":
                cell.value.ReadFromJson(reader);
                break;
                case "optional":
                if (reader.TokenType == JsonToken.Null) {
                    optional = null;
                }
                else { 
                    if (optional == null) {
                        optional = new ZergRush.CodeGen.Tests.JsonExistingPlain();
                    }
                    optional.ReadFromJson(reader);
                }
                break;
                case "required":
                required.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("cell");
            cell.value.WriteJson(writer);
            if (!(optional != null))
            {
                writer.WritePropertyName("optional");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("optional");
                optional.WriteJson(writer);
            }
            writer.WritePropertyName("required");
            required.WriteJson(writer);
        }
    }
}
#endif

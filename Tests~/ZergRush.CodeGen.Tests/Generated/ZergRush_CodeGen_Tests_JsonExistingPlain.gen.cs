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

    public partial class JsonExistingPlain : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonExistingPlain>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonExistingPlain>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonExistingPlain other, ZRUpdateFromHelper __helper) 
        {
            isRef = other.isRef;
            refId = other.refId;
            score = other.score;
            values.UpdateFrom(other.values, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            isRef = reader.ReadBoolean();
            refId = reader.ReadString();
            score = reader.ReadInt32();
            values.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(isRef);
            writer.Write(refId);
            writer.Write(score);
            values.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1277303742;
            hash += hash << 11; hash ^= hash >> 7;
            hash += isRef ? 1u : 0u;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(refId);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            hash += values.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonExistingPlain other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (isRef != other.isRef) CodeGenImplTools.LogCompError(__helper, "isRef", printer, other.isRef, isRef);
            if (refId != other.refId) CodeGenImplTools.LogCompError(__helper, "refId", printer, other.refId, refId);
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
            __helper.Push("values");
            values.CompareCheck(other.values, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "isRef":
                isRef = (bool)reader.Value;
                break;
                case "refId":
                refId = (string) reader.Value;
                break;
                case "score":
                score = (int)(Int64)reader.Value;
                break;
                case "values":
                values.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("isRef");
            writer.WriteValue(isRef);
            writer.WritePropertyName("refId");
            writer.WriteValue(refId);
            writer.WritePropertyName("score");
            writer.WriteValue(score);
            writer.WritePropertyName("values");
            values.WriteJson(writer);
        }
    }
}
#endif

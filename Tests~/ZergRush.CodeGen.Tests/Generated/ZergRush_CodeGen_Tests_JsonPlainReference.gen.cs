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

    public partial class JsonPlainReference : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonPlainReference>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonPlainReference>, IJsonSerializable, IsMultiRef
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonPlainReference other, ZRUpdateFromHelper __helper) 
        {
            var __next = next;
            if (other.next == null) {
                __next = null;
            }
            else { 
                if (__next == null) {
                    __next = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.next, ref __next)) __next.UpdateFrom(other.next, __helper);
            }
            next = __next;
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            var __next = next;
            if (!reader.ReadBoolean()) {
                __next = null;
            }
            else { 
                if (__next == null) {
                    __next = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                reader.ReadFromRef(ref __next);
            }
            next = __next;
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            if (!(next != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.WriteObjectWithRef(next);
            }
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)342241146;
            hash += hash << 11; hash ^= hash >> 7;
            hash += next != null ? __helper.CalculateHash(next) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonPlainReference other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (CodeGenImplTools.CompareNull(__helper, "next", printer, next, other.next)) {
                if (__helper.NeedCompareCheck("next", printer, next, other.next)) {
                    __helper.Push("next");
                    next.CompareCheck(other.next, __helper, printer);
                    __helper.Pop();
                }
            }
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "next":
                if (reader.TokenType == JsonToken.Null) {
                    next = null;
                }
                else { 
                    var __nextFrame = reader.BeginObject(false, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPlainReference>(__nextFrame, out var __nextFrameValue)) next = __nextFrameValue;
                    else
                    {
                        next = new ZergRush.CodeGen.Tests.JsonPlainReference();
                    }
                    reader.ReadObject(next, __nextFrame);
                }
                break;
                case "score":
                score = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            if (!(next != null))
            {
                writer.WritePropertyName("next");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("next");
                writer.WriteObjectWithRef(next);
            }
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
    }
}
#endif

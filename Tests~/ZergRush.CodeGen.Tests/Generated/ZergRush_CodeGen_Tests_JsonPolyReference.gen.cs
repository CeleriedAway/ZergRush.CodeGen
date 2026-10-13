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

    public partial class JsonPolyReference : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonPolyReference>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonPolyReference>, IJsonSerializable, IsMultiRef, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            JsonPolyReference = 1,
            JsonPolyReferenceChild = 2,
        }
        static Func<JsonPolyReference> [] polymorphConstructors = new Func<JsonPolyReference> [] {
            () => null, // 0
            () => new ZergRush.CodeGen.Tests.JsonPolyReference(), // 1
            () => new ZergRush.CodeGen.Tests.JsonPolyReferenceChild(), // 2
        };
        public static JsonPolyReference CreatePolymorphic(ushort typeId) {
            if (typeId >= polymorphConstructors.Length) throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
            return polymorphConstructors[typeId]() ?? throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
        }
        public JsonPolyReferenceType type => (JsonPolyReferenceType) GetClassId();
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonPolyReference other, ZRUpdateFromHelper __helper) 
        {
            var __next = next;
            if (other.next == null) {
                __next = null;
            }
            else { 
                var __nextClassId = other.next.GetClassId();
                if (__next == null || __next.GetClassId() != __nextClassId) {
                    __next = (ZergRush.CodeGen.Tests.JsonPolyReference)other.next.NewInst();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.next, ref __next)) __next.UpdateFrom(other.next, __helper);
            }
            next = __next;
            var __owner = owner;
            if (other.owner == null) {
                __owner = null;
            }
            else { 
                if (__owner == null) {
                    __owner = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.owner, ref __owner)) __owner.UpdateFrom(other.owner, __helper);
            }
            owner = __owner;
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            var __next = next;
            if (!reader.ReadBoolean()) {
                __next = null;
            }
            else { 
                var __nextClassId = reader.ReadUInt16();
                if (__next == null || __next.GetClassId() != __nextClassId) {
                    __next = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__nextClassId);
                }
                reader.ReadFromRef(ref __next);
            }
            next = __next;
            var __owner = owner;
            if (!reader.ReadBoolean()) {
                __owner = null;
            }
            else { 
                if (__owner == null) {
                    __owner = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                }
                reader.ReadFromRef(ref __owner);
            }
            owner = __owner;
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            if (!(next != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(next.GetClassId());
                writer.WriteObjectWithRef(next);
            }
            if (!(owner != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.WriteObjectWithRef(owner);
            }
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)116872478;
            hash += hash << 11; hash ^= hash >> 7;
            hash += next != null ? __helper.CalculateHash(next) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += owner != null ? __helper.CalculateHash(owner) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonPolyReference other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (CodeGenImplTools.CompareNull(__helper, "next", printer, next, other.next)) {
                if (CodeGenImplTools.CompareClassId(__helper, "next", printer, next, other.next)) {
                    if (__helper.NeedCompareCheck("next", printer, next, other.next)) {
                        __helper.Push("next");
                        next.CompareCheck(other.next, __helper, printer);
                        __helper.Pop();
                    }
                }
            }
            if (CodeGenImplTools.CompareNull(__helper, "owner", printer, owner, other.owner)) {
                if (__helper.NeedCompareCheck("owner", printer, owner, other.owner)) {
                    __helper.Push("owner");
                    owner.CompareCheck(other.owner, __helper, printer);
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
                    var __nextFrame = reader.BeginObject(true, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPolyReference>(__nextFrame, out var __nextFrameValue)) next = __nextFrameValue;
                    else
                    {
                        next = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__nextFrame.ClassId);
                    }
                    reader.ReadObject(next, __nextFrame);
                }
                break;
                case "owner":
                if (reader.TokenType == JsonToken.Null) {
                    owner = null;
                }
                else { 
                    var __ownerFrame = reader.BeginObject(false, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonReferenceRoot>(__ownerFrame, out var __ownerFrameValue)) owner = __ownerFrameValue;
                    else
                    {
                        owner = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                    }
                    reader.ReadObject(owner, __ownerFrame);
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
            if (!(owner != null))
            {
                writer.WritePropertyName("owner");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("owner");
                writer.WriteObjectWithRef(owner);
            }
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.JsonPolyReference;
        }
        public virtual object NewInst() 
        {
        return new JsonPolyReference();
        }
        public static ZergRush.CodeGen.Tests.JsonPolyReference CreatePolymorphic(JsonPolyReferenceType __classId) 
        {
        return (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif

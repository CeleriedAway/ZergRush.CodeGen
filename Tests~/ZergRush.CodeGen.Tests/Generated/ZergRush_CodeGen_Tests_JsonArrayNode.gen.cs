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

    public partial class JsonArrayNode : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonArrayNode>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonArrayNode>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            JsonArrayNode = 1,
            JsonArrayChild = 2,
        }
        static Func<JsonArrayNode> [] polymorphConstructors = new Func<JsonArrayNode> [] {
            () => null, // 0
            () => new ZergRush.CodeGen.Tests.JsonArrayNode(), // 1
            () => new ZergRush.CodeGen.Tests.JsonArrayChild(), // 2
        };
        public static JsonArrayNode CreatePolymorphic(ushort typeId) {
            if (typeId >= polymorphConstructors.Length) throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
            return polymorphConstructors[typeId]() ?? throw new ZergRushException("Unknown polymorphic class ID: " + typeId);
        }
        public JsonArrayNodeType type => (JsonArrayNodeType) GetClassId();
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonArrayNode other, ZRUpdateFromHelper __helper) 
        {
            date = other.date;
            label = other.label;
            if (other.nested == null) {
                nested = null;
            }
            else { 
                var nestedClassId = other.nested.GetClassId();
                if (nested == null || nested.GetClassId() != nestedClassId) {
                    nested = (ZergRush.CodeGen.Tests.JsonArrayNode)other.nested.NewInst();
                }
                nested.UpdateFrom(other.nested, __helper);
            }
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            date = reader.ReadDateTime();
            label = reader.ReadString();
            if (!reader.ReadBoolean()) {
                nested = null;
            }
            else { 
                var nestedClassId = reader.ReadUInt16();
                if (nested == null || nested.GetClassId() != nestedClassId) {
                    nested = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(nestedClassId);
                }
                nested.Deserialize(reader);
            }
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(date);
            writer.Write(label);
            if (!(nested != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nested.GetClassId());
                nested.Serialize(writer);
            }
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1687993347;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)date.Ticks;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(label);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nested != null ? nested.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonArrayNode other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (date != other.date) CodeGenImplTools.LogCompError(__helper, "date", printer, other.date, date);
            if (label != other.label) CodeGenImplTools.LogCompError(__helper, "label", printer, other.label, label);
            if (CodeGenImplTools.CompareNull(__helper, "nested", printer, nested, other.nested)) {
                if (CodeGenImplTools.CompareClassId(__helper, "nested", printer, nested, other.nested)) {
                    __helper.Push("nested");
                    nested.CompareCheck(other.nested, __helper, printer);
                    __helper.Pop();
                }
            }
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "date":
                date = new DateTime((Int64)reader.Value);
                break;
                case "label":
                label = (string) reader.Value;
                break;
                case "nested":
                if (reader.TokenType == JsonToken.Null) {
                    nested = null;
                }
                else { 
                    var __nestedFrame = reader.BeginObject(true, false);
                    var nestedClassId = __nestedFrame.ClassId;
                    if (nested == null || nested.GetClassId() != nestedClassId) {
                        nested = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(nestedClassId);
                    }
                    reader.ReadObject(nested, __nestedFrame);
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
            writer.WritePropertyName("date");
            writer.WriteValue(date.Ticks);
            writer.WritePropertyName("label");
            writer.WriteValue(label);
            if (!(nested != null))
            {
                writer.WritePropertyName("nested");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nested");
                nested.WriteJson(writer);
            }
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.JsonArrayNode;
        }
        public virtual object NewInst() 
        {
        return new JsonArrayNode();
        }
        public static ZergRush.CodeGen.Tests.JsonArrayNode CreatePolymorphic(JsonArrayNodeType __classId) 
        {
        return (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif

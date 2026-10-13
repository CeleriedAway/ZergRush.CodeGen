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

    public partial class JsonArrayHolder : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonArrayHolder>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonArrayHolder>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonArrayHolder other, ZRUpdateFromHelper __helper) 
        {
            var __arrayCount = other.array.Length;
            var __arrayTemp = array;
            Array.Resize(ref __arrayTemp, __arrayCount);
            array = __arrayTemp;
            array.UpdateFrom(other.array, __helper);
            var __cell_value = cell.value;
            if (other.cell.value == null) {
                __cell_value = null;
            }
            else { 
                var __cell_valueClassId = other.cell.value.GetClassId();
                if (__cell_value == null || __cell_value.GetClassId() != __cell_valueClassId) {
                    __cell_value = (ZergRush.CodeGen.Tests.JsonArrayNode)other.cell.value.NewInst();
                }
                __cell_value.UpdateFrom(other.cell.value, __helper);
            }
            cell.value = __cell_value;
            dictionary.UpdateFrom(other.dictionary, __helper);
            if (other.item == null) {
                item = null;
            }
            else { 
                var itemClassId = other.item.GetClassId();
                if (item == null || item.GetClassId() != itemClassId) {
                    item = (ZergRush.CodeGen.Tests.JsonArrayNode)other.item.NewInst();
                }
                item.UpdateFrom(other.item, __helper);
            }
            items.UpdateFrom(other.items, __helper);
            reactive.UpdateFrom(other.reactive, __helper);
            tail = other.tail;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            array = reader.ReadZergRush_CodeGen_Tests_JsonArrayNode_Array();
            if (!reader.ReadBoolean()) {
                cell.value = null;
            }
            else { 
                var cell_valueClassId = reader.ReadUInt16();
                if (cell.value == null || cell.value.GetClassId() != cell_valueClassId) {
                    cell.value = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(cell_valueClassId);
                }
                cell.value.Deserialize(reader);
            }
            dictionary.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                item = null;
            }
            else { 
                var itemClassId = reader.ReadUInt16();
                if (item == null || item.GetClassId() != itemClassId) {
                    item = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(itemClassId);
                }
                item.Deserialize(reader);
            }
            items.Deserialize(reader);
            reactive.Deserialize(reader);
            tail = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            array.Serialize(writer);
            if (!(cell.value != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(cell.value.GetClassId());
                cell.value.Serialize(writer);
            }
            dictionary.Serialize(writer);
            if (!(item != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(item.GetClassId());
                item.Serialize(writer);
            }
            items.Serialize(writer);
            reactive.Serialize(writer);
            writer.Write(tail);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1098887242;
            hash += hash << 11; hash ^= hash >> 7;
            hash += array.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += cell.value != null ? cell.value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += dictionary.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += item != null ? item.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += items.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactive.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)tail;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  JsonArrayHolder() 
        {
            array = Array.Empty<ZergRush.CodeGen.Tests.JsonArrayNode>();
            cell = new ZergRush.ReactiveCore.Cell<ZergRush.CodeGen.Tests.JsonArrayNode>();
            dictionary = new System.Collections.Generic.Dictionary<string, ZergRush.CodeGen.Tests.JsonArrayNode>();
            items = new System.Collections.Generic.List<ZergRush.CodeGen.Tests.JsonArrayNode>();
            reactive = new ZergRush.ReactiveCore.ReactiveCollection<ZergRush.CodeGen.Tests.JsonArrayNode>();
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonArrayHolder other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("array");
            array.CompareCheck(other.array, __helper, printer);
            __helper.Pop();
            if (CodeGenImplTools.CompareNull(__helper, "cell", printer, cell.value, other.cell.value)) {
                if (CodeGenImplTools.CompareClassId(__helper, "cell", printer, cell.value, other.cell.value)) {
                    __helper.Push("cell");
                    cell.value.CompareCheck(other.cell.value, __helper, printer);
                    __helper.Pop();
                }
            }
            __helper.Push("dictionary");
            dictionary.CompareCheck(other.dictionary, __helper, printer);
            __helper.Pop();
            if (CodeGenImplTools.CompareNull(__helper, "item", printer, item, other.item)) {
                if (CodeGenImplTools.CompareClassId(__helper, "item", printer, item, other.item)) {
                    __helper.Push("item");
                    item.CompareCheck(other.item, __helper, printer);
                    __helper.Pop();
                }
            }
            __helper.Push("items");
            items.CompareCheck(other.items, __helper, printer);
            __helper.Pop();
            __helper.Push("reactive");
            reactive.CompareCheck(other.reactive, __helper, printer);
            __helper.Pop();
            if (tail != other.tail) CodeGenImplTools.LogCompError(__helper, "tail", printer, other.tail, tail);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "array":
                array = array.ReadFromJson(reader);
                break;
                case "cell":
                if (reader.TokenType == JsonToken.Null) {
                    cell.value = null;
                }
                else { 
                    var __cell_valueFrame = reader.BeginObject(true, false);
                    var cell_valueClassId = __cell_valueFrame.ClassId;
                    if (cell.value == null || cell.value.GetClassId() != cell_valueClassId) {
                        cell.value = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(cell_valueClassId);
                    }
                    reader.ReadObject(cell.value, __cell_valueFrame);
                }
                break;
                case "dictionary":
                dictionary.ReadFromJson(reader);
                break;
                case "item":
                if (reader.TokenType == JsonToken.Null) {
                    item = null;
                }
                else { 
                    var __itemFrame = reader.BeginObject(true, false);
                    var itemClassId = __itemFrame.ClassId;
                    if (item == null || item.GetClassId() != itemClassId) {
                        item = (ZergRush.CodeGen.Tests.JsonArrayNode)ZergRush.CodeGen.Tests.JsonArrayNode.CreatePolymorphic(itemClassId);
                    }
                    reader.ReadObject(item, __itemFrame);
                }
                break;
                case "items":
                items.ReadFromJson(reader);
                break;
                case "reactive":
                reactive.ReadFromJson(reader);
                break;
                case "tail":
                tail = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("array");
            array.WriteJson(writer);
            if (!(cell.value != null))
            {
                writer.WritePropertyName("cell");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("cell");
                cell.value.WriteJson(writer);
            }
            writer.WritePropertyName("dictionary");
            dictionary.WriteJson(writer);
            if (!(item != null))
            {
                writer.WritePropertyName("item");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("item");
                item.WriteJson(writer);
            }
            writer.WritePropertyName("items");
            items.WriteJson(writer);
            writer.WritePropertyName("reactive");
            reactive.WriteJson(writer);
            writer.WritePropertyName("tail");
            writer.WriteValue(tail);
        }
    }
}
#endif

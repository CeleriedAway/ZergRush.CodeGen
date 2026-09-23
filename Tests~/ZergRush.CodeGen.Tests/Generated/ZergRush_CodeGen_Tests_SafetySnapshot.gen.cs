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

    public partial class SafetySnapshot : IUpdatableFrom<ZergRush.CodeGen.Tests.SafetySnapshot>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.SafetySnapshot>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.SafetySnapshot other, ZRUpdateFromHelper __helper) 
        {
            var __arrayCount = other.array.Length;
            var __arrayTemp = array;
            Array.Resize(ref __arrayTemp, __arrayCount);
            array = __arrayTemp;
            array.UpdateFrom(other.array, __helper);
            idToPlace.UpdateFrom(other.idToPlace, __helper);
            var __intsCount = other.ints.Length;
            var __intsTemp = ints;
            Array.Resize(ref __intsTemp, __intsCount);
            ints = __intsTemp;
            global::ZergRush.Int32ArraySerialization.UpdateFrom(ints, other.ints, __helper);
            leaderboard.UpdateFrom(other.leaderboard, __helper);
            list.UpdateFrom(other.list, __helper);
            nullableEntries.UpdateFrom(other.nullableEntries, __helper);
            reactiveValues.UpdateFrom(other.reactiveValues, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            array = reader.ReadSystem_Int64_Array();
            idToPlace.Deserialize(reader);
            ints = global::ZergRush.Int32ArraySerialization.ReadSystem_Int32_Array(reader);
            leaderboard.Deserialize(reader);
            list.Deserialize(reader);
            nullableEntries.Deserialize(reader);
            reactiveValues.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            array.Serialize(writer);
            idToPlace.Serialize(writer);
            global::ZergRush.Int32ArraySerialization.Serialize(ints, writer);
            leaderboard.Serialize(writer);
            list.Serialize(writer);
            nullableEntries.Serialize(writer);
            reactiveValues.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1811012539;
            hash += hash << 11; hash ^= hash >> 7;
            hash += array.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += idToPlace.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += global::ZergRush.Int32ArraySerialization.CalculateHash(ints, __helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += leaderboard.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += list.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableEntries.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveValues.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  SafetySnapshot() 
        {
            array = Array.Empty<long>();
            idToPlace = new System.Collections.Generic.Dictionary<long, int>();
            ints = Array.Empty<int>();
            leaderboard = new SimpleList<ZergRush.CodeGen.Tests.SafetyEntry>();
            list = new System.Collections.Generic.List<int>();
            nullableEntries = new System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry>();
            reactiveValues = new ZergRush.ReactiveCore.ReactiveCollection<int>();
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.SafetySnapshot other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("array");
            array.CompareCheck(other.array, __helper, printer);
            __helper.Pop();
            __helper.Push("idToPlace");
            idToPlace.CompareCheck(other.idToPlace, __helper, printer);
            __helper.Pop();
            __helper.Push("ints");
            global::ZergRush.Int32ArraySerialization.CompareCheck(ints, other.ints, __helper, printer);
            __helper.Pop();
            __helper.Push("leaderboard");
            leaderboard.CompareCheck(other.leaderboard, __helper, printer);
            __helper.Pop();
            __helper.Push("list");
            list.CompareCheck(other.list, __helper, printer);
            __helper.Pop();
            __helper.Push("nullableEntries");
            nullableEntries.CompareCheck(other.nullableEntries, __helper, printer);
            __helper.Pop();
            __helper.Push("reactiveValues");
            reactiveValues.CompareCheck(other.reactiveValues, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "array":
                array = array.ReadFromJson(reader);
                break;
                case "idToPlace":
                idToPlace.ReadFromJson(reader);
                break;
                case "ints":
                ints = global::ZergRush.Int32ArraySerialization.ReadFromJson(ints, reader);
                break;
                case "leaderboard":
                leaderboard.ReadFromJson(reader);
                break;
                case "list":
                list.ReadFromJson(reader);
                break;
                case "nullableEntries":
                nullableEntries.ReadFromJson(reader);
                break;
                case "reactiveValues":
                reactiveValues.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("array");
            array.WriteJson(writer);
            writer.WritePropertyName("idToPlace");
            idToPlace.WriteJson(writer);
            writer.WritePropertyName("ints");
            global::ZergRush.Int32ArraySerialization.WriteJson(ints, writer);
            writer.WritePropertyName("leaderboard");
            leaderboard.WriteJson(writer);
            writer.WritePropertyName("list");
            list.WriteJson(writer);
            writer.WritePropertyName("nullableEntries");
            nullableEntries.WriteJson(writer);
            writer.WritePropertyName("reactiveValues");
            reactiveValues.WriteJson(writer);
        }
    }
}
#endif

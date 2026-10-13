// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZergRush.Alive;
using ZergRush;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION

public static partial class SerializationExtensions
{
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            var __self_i_ = self[i];
            if (other[i] == null) {
                __self_i_ = null;
            }
            else { 
                var __self_i_ClassId = other[i].GetClassId();
                if (__self_i_ == null || __self_i_.GetClassId() != __self_i_ClassId) {
                    __self_i_ = (ZergRush.JsonBenchmarks.BenchReference)other[i].NewInst();
                }
                if (!__helper.TryLoadAlreadyUpdated(other[i], ref __self_i_)) __self_i_.UpdateFrom(other[i], __helper);
            }
            self[i] = __self_i_;
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.JsonBenchmarks.BenchReference inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = (ZergRush.JsonBenchmarks.BenchReference)other[i].NewInst();
                if (!__helper.TryLoadAlreadyUpdated(other[i], ref inst)) inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.JsonBenchmarks.BenchReference>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.JsonBenchmarks.BenchReference val = default;
            val = (ZergRush.JsonBenchmarks.BenchReference)ZergRush.JsonBenchmarks.BenchReference.CreatePolymorphic(reader.ReadUInt16());
            reader.ReadFromRef(ref val);
            self.Add(val);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(self[i].GetClassId());
                writer.WriteObjectWithRef(self[i]);
            }
        }
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i] != null ? __helper.CalculateHash(self[i]) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                if (CodeGenImplTools.CompareClassId(__helper, i.ToString(), printer, self[i], other[i])) {
                    if (__helper.NeedCompareCheck(i.ToString(), printer, self[i], other[i])) {
                        __helper.Push(i.ToString());
                        self[i].CompareCheck(other[i], __helper, printer);
                        __helper.Pop();
                    }
                }
            }
        }
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.JsonBenchmarks.BenchReference>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.JsonBenchmarks.BenchReference val = default;
            var __valFrame = reader.BeginObject(true, true);
            if (reader.TryGetReference<ZergRush.JsonBenchmarks.BenchReference>(__valFrame, out var __valFrameValue)) val = __valFrameValue;
            else
            {
                val = (ZergRush.JsonBenchmarks.BenchReference)ZergRush.JsonBenchmarks.BenchReference.CreatePolymorphic(__valFrame.ClassId);
            }
            reader.ReadObject(val, __valFrame);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchReference> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null))
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteObjectWithRef(self[i]);
            }
        }
        writer.WriteEndArray();
    }
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            if (other[i] == null) {
                self[i] = null;
            }
            else { 
                var self_i_ClassId = other[i].GetClassId();
                if (self[i] == null || self[i].GetClassId() != self_i_ClassId) {
                    self[i] = (ZergRush.JsonBenchmarks.BenchValue)other[i].NewInst();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.JsonBenchmarks.BenchValue inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = (ZergRush.JsonBenchmarks.BenchValue)other[i].NewInst();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.JsonBenchmarks.BenchValue>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.JsonBenchmarks.BenchValue val = default;
            val = (ZergRush.JsonBenchmarks.BenchValue)ZergRush.JsonBenchmarks.BenchValue.CreatePolymorphic(reader.ReadUInt16());
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(self[i].GetClassId());
                self[i].Serialize(writer);
            }
        }
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i] != null ? self[i].CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                if (CodeGenImplTools.CompareClassId(__helper, i.ToString(), printer, self[i], other[i])) {
                    __helper.Push(i.ToString());
                    self[i].CompareCheck(other[i], __helper, printer);
                    __helper.Pop();
                }
            }
        }
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.JsonBenchmarks.BenchValue>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.JsonBenchmarks.BenchValue val = default;
            var __valFrame = reader.BeginObject(true, false);
            val = (ZergRush.JsonBenchmarks.BenchValue)ZergRush.JsonBenchmarks.BenchValue.CreatePolymorphic(__valFrame.ClassId);
            reader.ReadObject(val, __valFrame);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.JsonBenchmarks.BenchValue> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null))
            {
                writer.WriteNull();
            }
            else
            {
                self[i].WriteJson(writer);
            }
        }
        writer.WriteEndArray();
    }
}
#endif

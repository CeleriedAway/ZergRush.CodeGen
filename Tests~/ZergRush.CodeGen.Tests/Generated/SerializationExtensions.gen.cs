// ZergRush serialization schema: 2 (logical collections, replacement reads, strict JSON)
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
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> other, ZRUpdateFromHelper __helper) 
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
                    self[i] = (ZergRush.Samples.CodeGenSamples)other[i].NewInst();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.Samples.CodeGenSamples inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = (ZergRush.Samples.CodeGenSamples)other[i].NewInst();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this ZergRush.Samples.ExternalClass self, ZergRush.Samples.ExternalClass other, ZRUpdateFromHelper __helper) 
    {
        self.somePublicField = other.somePublicField;
    }
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> other, ZRUpdateFromHelper __helper) 
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
                    self[i] = (ZergRush.Samples.TestPolyGenericParent)other[i].NewInst();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.Samples.TestPolyGenericParent inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = (ZergRush.Samples.TestPolyGenericParent)other[i].NewInst();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> other, ZRUpdateFromHelper __helper) 
    {
        var __self_reactiveValue_value = self.reactiveValue.value;
        __self_reactiveValue_value.UpdateFrom(other.reactiveValue.value, __helper);
        self.reactiveValue.value = __self_reactiveValue_value;
        self.value.UpdateFrom(other.value, __helper);
        self.values.UpdateFrom(other.values, __helper);
        self.valuesByName.UpdateFrom(other.valuesByName, __helper);
    }
    public static void UpdateFrom(this ZergRush.Samples.TestGeneric<int> self, ZergRush.Samples.TestGeneric<int> other, ZRUpdateFromHelper __helper) 
    {
        self.reactiveValue.value = other.reactiveValue.value;
        self.value = other.value;
        self.values.UpdateFrom(other.values, __helper);
        self.valuesByName.UpdateFrom(other.valuesByName, __helper);
    }
    public static void UpdateFrom(this System.Collections.Generic.List<int?> self, System.Collections.Generic.List<int?> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, System.Collections.Generic.List<ZergRush.Samples.OtherData> other, ZRUpdateFromHelper __helper) 
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
                if (self[i] == null) {
                    self[i] = new ZergRush.Samples.OtherData();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.Samples.OtherData inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = new ZergRush.Samples.OtherData();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.List<int> self, System.Collections.Generic.List<int> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(ref this ZergRush.Samples.PlainStruct self, ZergRush.Samples.PlainStruct other, ZRUpdateFromHelper __helper) 
    {
        self.position.UpdateFrom(other.position, __helper);
        self.value = other.value;
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.Cell<ZergRush.Samples.OtherData> self, ZergRush.ReactiveCore.Cell<ZergRush.Samples.OtherData> other, ZRUpdateFromHelper __helper) 
    {

    }
    public static void UpdateFrom(ref this UnityEngine.Vector3 self, UnityEngine.Vector3 other, ZRUpdateFromHelper __helper) 
    {
        self.x = other.x;
        self.y = other.y;
        self.z = other.z;
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZergRush.ReactiveCore.ReactiveCollection<int> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.Samples.CodeGenSamples>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.Samples.CodeGenSamples val = default;
            val = (ZergRush.Samples.CodeGenSamples)ZergRush.Samples.CodeGenSamples.CreatePolymorphic(reader.ReadUInt16());
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        reader.Budget.Reserve<System.Collections.Generic.List<System.Collections.Generic.List<string>>>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(int);
            key = reader.ReadInt32();
            if (!reader.ReadBoolean()) { self.Add(key, null); continue; }
            var val = default(System.Collections.Generic.List<System.Collections.Generic.List<string>>);
            val = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
            val.Deserialize(reader);
            self.Add(key, val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        reader.Budget.Reserve<ZergRush.Samples.OtherData>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(int);
            key = reader.ReadInt32();
            if (!reader.ReadBoolean()) { self.Add(key, null); continue; }
            var val = default(ZergRush.Samples.OtherData);
            val = new ZergRush.Samples.OtherData();
            val.Deserialize(reader);
            self.Add(key, val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<string, int?> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        reader.Budget.Reserve<int?>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(string);
            key = string.Empty;
            key = reader.ReadString();
            var val = default(int?);
            if (!reader.ReadBoolean()) {
                val = null;
            }
            else {
                var __val = val.GetValueOrDefault();
                __val = reader.ReadInt32();
                val = __val;
            }
            self.Add(key, val);
        }
    }
    public static void Deserialize(this ZergRush.Samples.ExternalClass self, ZRBinaryReader reader) 
    {
        self.somePublicField = reader.ReadInt32();
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.Samples.TestPolyGenericParent>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.Samples.TestPolyGenericParent val = default;
            val = (ZergRush.Samples.TestPolyGenericParent)ZergRush.Samples.TestPolyGenericParent.CreatePolymorphic(reader.ReadUInt16());
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZRBinaryReader reader) 
    {
        self.reactiveValue.value = reader.ReadZergRush_Samples_CustomStruct();
        self.value = reader.ReadZergRush_Samples_CustomStruct();
        self.values.Deserialize(reader);
        self.valuesByName.Deserialize(reader);
    }
    public static void Deserialize(this ZergRush.Samples.TestGeneric<int> self, ZRBinaryReader reader) 
    {
        self.reactiveValue.value = reader.ReadInt32();
        self.value = reader.ReadInt32();
        self.values.Deserialize(reader);
        self.valuesByName.Deserialize(reader);
    }
    public static void Deserialize(this System.Collections.Generic.List<int?> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int?>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            int? val = default;
            if (!reader.ReadBoolean()) {
                val = null;
            }
            else {
                var __val = val.GetValueOrDefault();
                __val = reader.ReadInt32();
                val = __val;
            }
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.Samples.OtherData>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.Samples.OtherData val = default;
            val = new ZergRush.Samples.OtherData();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<int> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            int val = default;
            val = reader.ReadInt32();
            self.Add(val);
        }
    }
    public static ZergRush.Samples.PlainStruct ReadZergRush_Samples_PlainStruct(this ZRBinaryReader reader) 
    {
        var self = default(ZergRush.Samples.PlainStruct);
        self.position = reader.ReadUnityEngine_Vector3();
        self.value = reader.ReadInt32();
        return self;
    }
    public static UnityEngine.Vector3 ReadUnityEngine_Vector3(this ZRBinaryReader reader) 
    {
        var self = default(UnityEngine.Vector3);
        self.x = reader.ReadSingle();
        self.y = reader.ReadSingle();
        self.z = reader.ReadSingle();
        return self;
    }
    public static void Deserialize(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            int val = default;
            val = reader.ReadInt32();
            self.Add(val);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, ZRBinaryWriter writer) 
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
    public static void Serialize(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value != null)) writer.Write(false);
            else {
                writer.Write(true);
                item.Value.Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value != null)) writer.Write(false);
            else {
                writer.Write(true);
                item.Value.Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<string, int?> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(item.Value.Value);
            }
        }
    }
    public static void Serialize(this ZergRush.Samples.ExternalClass self, ZRBinaryWriter writer) 
    {
        writer.Write(self.somePublicField);
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, ZRBinaryWriter writer) 
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
    public static void Serialize(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZRBinaryWriter writer) 
    {
        self.reactiveValue.value.Serialize(writer);
        self.value.Serialize(writer);
        self.values.Serialize(writer);
        self.valuesByName.Serialize(writer);
    }
    public static void Serialize(this ZergRush.Samples.TestGeneric<int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.reactiveValue.value);
        writer.Write(self.value);
        self.values.Serialize(writer);
        self.valuesByName.Serialize(writer);
    }
    public static void Serialize(this System.Collections.Generic.List<int?> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i].HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(self[i].Value);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.List<int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            writer.Write(self[i]);
        }
    }
    public static void Serialize(this ZergRush.Samples.PlainStruct self, ZRBinaryWriter writer) 
    {
        self.position.Serialize(writer);
        writer.Write(self.value);
    }
    public static void Serialize(this UnityEngine.Vector3 self, ZRBinaryWriter writer) 
    {
        writer.Write(self.x);
        writer.Write(self.y);
        writer.Write(self.z);
    }
    public static void Serialize(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            writer.Write(self[i]);
        }
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, ZRHashHelper __helper) 
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
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += (ulong)item.Key;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += (ulong)item.Key;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<string, int?> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += item.Key != null ? CodeGenImplTools.CalculateStringHash(item.Key) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.HasValue ? (ulong)item.Value.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this ZergRush.Samples.ExternalClass self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)840039565;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)self.somePublicField;
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, ZRHashHelper __helper) 
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
    public static ulong CalculateHash(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)1068104782;
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.reactiveValue.value.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.value.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.values.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.valuesByName.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this ZergRush.Samples.TestGeneric<int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)1068104782;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)self.reactiveValue.value;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)self.value;
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.values.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.valuesByName.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<int?> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i].HasValue ? (ulong)self[i].Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, ZRHashHelper __helper) 
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
    public static ulong CalculateHash(this System.Collections.Generic.List<int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += (ulong)self[i];
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this ZergRush.Samples.PlainStruct self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)1251762542;
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.position.CalculateHash(__helper);
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)self.value;
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this UnityEngine.Vector3 self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)701202043;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.x);
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.y);
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.z);
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)1025231816;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += (ulong)self[i];
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> other, ZRCompareCheckHelper __helper, Action<string> printer) 
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
    public static void CompareCheck(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (CodeGenImplTools.CompareNull(__helper, item.Key.ToString(), printer, item.Value, otherValue)) {
                    __helper.Push(item.Key.ToString());
                    item.Value.CompareCheck(otherValue, __helper, printer);
                    __helper.Pop();
                }
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (CodeGenImplTools.CompareNull(__helper, item.Key.ToString(), printer, item.Value, otherValue)) {
                    __helper.Push(item.Key.ToString());
                    item.Value.CompareCheck(otherValue, __helper, printer);
                    __helper.Pop();
                }
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<string, int?> self, System.Collections.Generic.Dictionary<string, int?> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (item.Value != otherValue) CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, otherValue, item.Value);
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static void CompareCheck(this ZergRush.Samples.ExternalClass self, ZergRush.Samples.ExternalClass other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.somePublicField != other.somePublicField) CodeGenImplTools.LogCompError(__helper, "somePublicField", printer, other.somePublicField, self.somePublicField);
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> other, ZRCompareCheckHelper __helper, Action<string> printer) 
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
    public static void CompareCheck(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        __helper.Push("reactiveValue");
        self.reactiveValue.value.CompareCheck(other.reactiveValue.value, __helper, printer);
        __helper.Pop();
        __helper.Push("value");
        self.value.CompareCheck(other.value, __helper, printer);
        __helper.Pop();
        __helper.Push("values");
        self.values.CompareCheck(other.values, __helper, printer);
        __helper.Pop();
        __helper.Push("valuesByName");
        self.valuesByName.CompareCheck(other.valuesByName, __helper, printer);
        __helper.Pop();
    }
    public static void CompareCheck(this ZergRush.Samples.TestGeneric<int> self, ZergRush.Samples.TestGeneric<int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.reactiveValue.value != other.reactiveValue.value) CodeGenImplTools.LogCompError(__helper, "reactiveValue", printer, other.reactiveValue.value, self.reactiveValue.value);
        if (self.value != other.value) CodeGenImplTools.LogCompError(__helper, "value", printer, other.value, self.value);
        __helper.Push("values");
        self.values.CompareCheck(other.values, __helper, printer);
        __helper.Pop();
        __helper.Push("valuesByName");
        self.valuesByName.CompareCheck(other.valuesByName, __helper, printer);
        __helper.Pop();
    }
    public static void CompareCheck(this System.Collections.Generic.List<int?> self, System.Collections.Generic.List<int?> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, System.Collections.Generic.List<ZergRush.Samples.OtherData> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                __helper.Push(i.ToString());
                self[i].CompareCheck(other[i], __helper, printer);
                __helper.Pop();
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.List<int> self, System.Collections.Generic.List<int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static void CompareCheck(this ZergRush.Samples.PlainStruct self, ZergRush.Samples.PlainStruct other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        __helper.Push("position");
        self.position.CompareCheck(other.position, __helper, printer);
        __helper.Pop();
        if (self.value != other.value) CodeGenImplTools.LogCompError(__helper, "value", printer, other.value, self.value);
    }
    public static void CompareCheck(this UnityEngine.Vector3 self, UnityEngine.Vector3 other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.x != other.x) CodeGenImplTools.LogCompError(__helper, "x", printer, other.x, self.x);
        if (self.y != other.y) CodeGenImplTools.LogCompError(__helper, "y", printer, other.y, self.y);
        if (self.z != other.z) CodeGenImplTools.LogCompError(__helper, "z", printer, other.z, self.z);
    }
    public static void CompareCheck(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZergRush.ReactiveCore.ReactiveCollection<int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.Samples.CodeGenSamples>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.Samples.CodeGenSamples val = default;
            val = (ZergRush.Samples.CodeGenSamples)ZergRush.Samples.CodeGenSamples.CreatePolymorphic(reader.ReadJsonClassId());
            val.ReadFromJson(reader);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.Samples.CodeGenSamples> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<int>(self.Count + 1);
            reader.Budget.ReserveElement<System.Collections.Generic.List<System.Collections.Generic.List<string>>>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            int key = default;
            key = (int)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            System.Collections.Generic.List<System.Collections.Generic.List<string>> val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else { 
                val = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
                val.ReadFromJson(reader);
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value != null))
            {
                writer.WriteNull();
            }
            else
            {
                item.Value.WriteJson(writer);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<int>(self.Count + 1);
            reader.Budget.ReserveElement<ZergRush.Samples.OtherData>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            int key = default;
            key = (int)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            ZergRush.Samples.OtherData val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else { 
                val = new ZergRush.Samples.OtherData();
                val.ReadFromJson(reader);
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value != null))
            {
                writer.WriteNull();
            }
            else
            {
                item.Value.WriteJson(writer);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<string, int?> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<string>(self.Count + 1);
            reader.Budget.ReserveElement<int?>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            string key = default;
            key = string.Empty;
            key = (string) reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            int? val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else {
                var __val = val.GetValueOrDefault();
                __val = (int)(Int64)reader.Value;
                val = __val;
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<string, int?> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value.HasValue))
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(item.Value.Value);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this ZergRush.Samples.ExternalClass self, ZRJsonTextReader reader) 
    {
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "somePublicField":
                    self.somePublicField = (int)(Int64)reader.Value;
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Samples.ExternalClass self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("somePublicField");
        writer.WriteValue(self.somePublicField);
        writer.WriteEndObject();
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.Samples.TestPolyGenericParent>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.Samples.TestPolyGenericParent val = default;
            val = (ZergRush.Samples.TestPolyGenericParent)ZergRush.Samples.TestPolyGenericParent.CreatePolymorphic(reader.ReadJsonClassId());
            val.ReadFromJson(reader);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZRJsonTextReader reader) 
    {
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "reactiveValue":
                    self.reactiveValue.value = (ZergRush.Samples.CustomStruct)reader.ReadFromJsonZergRush_Samples_CustomStruct();
                    break;
                    case "value":
                    self.value = (ZergRush.Samples.CustomStruct)reader.ReadFromJsonZergRush_Samples_CustomStruct();
                    break;
                    case "values":
                    self.values.ReadFromJson(reader);
                    break;
                    case "valuesByName":
                    self.valuesByName.ReadFromJson(reader);
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("reactiveValue");
        self.reactiveValue.value.WriteJson(writer);
        writer.WritePropertyName("value");
        self.value.WriteJson(writer);
        writer.WritePropertyName("values");
        self.values.WriteJson(writer);
        writer.WritePropertyName("valuesByName");
        self.valuesByName.WriteJson(writer);
        writer.WriteEndObject();
    }
    public static bool ReadFromJson(this ZergRush.Samples.TestGeneric<int> self, ZRJsonTextReader reader) 
    {
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "reactiveValue":
                    self.reactiveValue.value = (int)(Int64)reader.Value;
                    break;
                    case "value":
                    self.value = (int)(Int64)reader.Value;
                    break;
                    case "values":
                    self.values.ReadFromJson(reader);
                    break;
                    case "valuesByName":
                    self.valuesByName.ReadFromJson(reader);
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Samples.TestGeneric<int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("reactiveValue");
        writer.WriteValue(self.reactiveValue.value);
        writer.WritePropertyName("value");
        writer.WriteValue(self.value);
        writer.WritePropertyName("values");
        self.values.WriteJson(writer);
        writer.WritePropertyName("valuesByName");
        self.valuesByName.WriteJson(writer);
        writer.WriteEndObject();
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<int?> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<int?>(self.Count + 1);
            int? val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else {
                var __val = val.GetValueOrDefault();
                __val = (int)(Int64)reader.Value;
                val = __val;
            }
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<int?> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i].HasValue))
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(self[i].Value);
            }
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.Samples.OtherData>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.Samples.OtherData val = default;
            val = new ZergRush.Samples.OtherData();
            val.ReadFromJson(reader);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.Samples.OtherData> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this System.Collections.Generic.List<int> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<int>(self.Count + 1);
            int val = default;
            val = (int)(Int64)reader.Value;
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            writer.WriteValue(self[i]);
        }
        writer.WriteEndArray();
    }
    public static ZergRush.Samples.PlainStruct ReadFromJsonZergRush_Samples_PlainStruct(this ZRJsonTextReader reader) 
    {
        var self = default(ZergRush.Samples.PlainStruct);
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "position":
                    self.position = (UnityEngine.Vector3)reader.ReadFromJsonUnityEngine_Vector3();
                    break;
                    case "value":
                    self.value = (int)(Int64)reader.Value;
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return self;
    }
    public static void WriteJson(this ZergRush.Samples.PlainStruct self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("position");
        self.position.WriteJson(writer);
        writer.WritePropertyName("value");
        writer.WriteValue(self.value);
        writer.WriteEndObject();
    }
    public static UnityEngine.Vector3 ReadFromJsonUnityEngine_Vector3(this ZRJsonTextReader reader) 
    {
        var self = default(UnityEngine.Vector3);
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "x":
                    self.x = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    case "y":
                    self.y = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    case "z":
                    self.z = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return self;
    }
    public static void WriteJson(this UnityEngine.Vector3 self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(self.x);
        writer.WritePropertyName("y");
        writer.WriteValue(self.y);
        writer.WritePropertyName("z");
        writer.WriteValue(self.z);
        writer.WriteEndObject();
    }
    public static bool ReadFromJson(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<int>(self.Count + 1);
            int val = default;
            val = (int)(Int64)reader.Value;
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this ZergRush.ReactiveCore.ReactiveCollection<int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            writer.WriteValue(self[i]);
        }
        writer.WriteEndArray();
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> self, System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        int[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new int[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    if (__value == null) {
                        __value = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
                    }
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            else
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    __value = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            self[__pair.Key] = __value;
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.List<string> self, System.Collections.Generic.List<string> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, System.Collections.Generic.List<System.Collections.Generic.List<string>> other, ZRUpdateFromHelper __helper) 
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
                if (self[i] == null) {
                    self[i] = new System.Collections.Generic.List<string>();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            System.Collections.Generic.List<string> inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = new System.Collections.Generic.List<string>();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<string, int?> self, System.Collections.Generic.Dictionary<string, int?> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        string[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new string[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            self[__pair.Key] = __pair.Value;
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        string[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new string[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    if (__value == null) {
                        __value = new ZergRush.Samples.OtherData();
                    }
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            else
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    __value = new ZergRush.Samples.OtherData();
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            self[__pair.Key] = __value;
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<string, int> self, System.Collections.Generic.Dictionary<string, int> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        string[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new string[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            self[__pair.Key] = __pair.Value;
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<string> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            string val = default;
            val = string.Empty;
            val = reader.ReadString();
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<System.Collections.Generic.List<string>>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            System.Collections.Generic.List<string> val = default;
            val = new System.Collections.Generic.List<string>();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        reader.Budget.Reserve<ZergRush.Samples.OtherData>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(string);
            key = string.Empty;
            key = reader.ReadString();
            if (!reader.ReadBoolean()) { self.Add(key, null); continue; }
            var val = default(ZergRush.Samples.OtherData);
            val = new ZergRush.Samples.OtherData();
            val.Deserialize(reader);
            self.Add(key, val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<string, int> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        reader.Budget.Reserve<int>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(string);
            key = string.Empty;
            key = reader.ReadString();
            var val = default(int);
            val = reader.ReadInt32();
            self.Add(key, val);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<string> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(self[i]);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value != null)) writer.Write(false);
            else {
                writer.Write(true);
                item.Value.Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<string, int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            writer.Write(item.Value);
        }
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<string> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i] != null ? CodeGenImplTools.CalculateStringHash(self[i]) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, ZRHashHelper __helper) 
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
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += item.Key != null ? CodeGenImplTools.CalculateStringHash(item.Key) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<string, int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += item.Key != null ? CodeGenImplTools.CalculateStringHash(item.Key) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)item.Value;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this System.Collections.Generic.List<string> self, System.Collections.Generic.List<string> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static void CompareCheck(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, System.Collections.Generic.List<System.Collections.Generic.List<string>> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                __helper.Push(i.ToString());
                self[i].CompareCheck(other[i], __helper, printer);
                __helper.Pop();
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (CodeGenImplTools.CompareNull(__helper, item.Key.ToString(), printer, item.Value, otherValue)) {
                    __helper.Push(item.Key.ToString());
                    item.Value.CompareCheck(otherValue, __helper, printer);
                    __helper.Pop();
                }
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<string, int> self, System.Collections.Generic.Dictionary<string, int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (item.Value != otherValue) CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, otherValue, item.Value);
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<string> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<string>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            string val = default;
            val = string.Empty;
            val = (string) reader.Value;
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<string> self, ZRJsonTextWriter writer) 
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
                writer.WriteValue(self[i]);
            }
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<System.Collections.Generic.List<string>>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            System.Collections.Generic.List<string> val = default;
            val = new System.Collections.Generic.List<string>();
            val.ReadFromJson(reader);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<System.Collections.Generic.List<string>> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<string>(self.Count + 1);
            reader.Budget.ReserveElement<ZergRush.Samples.OtherData>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            string key = default;
            key = string.Empty;
            key = (string) reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            ZergRush.Samples.OtherData val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else { 
                val = new ZergRush.Samples.OtherData();
                val.ReadFromJson(reader);
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value != null))
            {
                writer.WriteNull();
            }
            else
            {
                item.Value.WriteJson(writer);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<string, int> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<string>(self.Count + 1);
            reader.Budget.ReserveElement<int>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            string key = default;
            key = string.Empty;
            key = (string) reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            int val = default;
            val = (int)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<string, int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            writer.WriteValue(item.Value);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static void UpdateFrom(this ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        string[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new string[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            self[__pair.Key] = __pair.Value;
        }
    }
    public static void UpdateFrom(this ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Deserialize(this ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(string);
            key = string.Empty;
            key = reader.ReadString();
            if (!reader.ReadBoolean()) { self.Add(key, null); continue; }
            var val = default(ZergRush.CodeGen.Tests.ConfigStorageCoverageItem);
            val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
            val.Deserialize(reader);
            ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            self.Add(key, val);
        }
    }
    public static void Deserialize(this ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.ConfigStorageCoverageItem val = default;
            val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
            val.Deserialize(reader);
            ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            self.Add(val);
        }
    }
    public static void Deserialize(this ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.ConfigStorageCoverageItem val = default;
            val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
            val.Deserialize(reader);
            ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            self.Add(val);
        }
    }
    public static void Serialize(this ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value != null)) writer.Write(false);
            else {
                writer.Write(true);
                item.Value.Serialize(writer);
            }
        }
    }
    public static void Serialize(this ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static void Serialize(this ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static bool ReadFromJson(this ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<string>(self.Count + 1);
            reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            string key = default;
            key = string.Empty;
            key = (string) reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            ZergRush.CodeGen.Tests.ConfigStorageCoverageItem val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else { 
                val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
                val.ReadFromJson(reader);
                ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value != null))
            {
                writer.WriteNull();
            }
            else
            {
                item.Value.WriteJson(writer);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.ConfigStorageCoverageItem val = default;
            val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
            val.ReadFromJson(reader);
            ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.ConfigStorageCoverageItem val = default;
            val = new ZergRush.CodeGen.Tests.ConfigStorageCoverageItem();
            val.ReadFromJson(reader);
            ZergRush.CodeGen.Tests.ConfigStorageCoverageRoot.Instance.RegisterConfig(val);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem> self, ZRJsonTextWriter writer) 
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
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> self, System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        int[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new int[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    if (__value == null) {
                        __value = new ZergRush.Samples.OtherData();
                    }
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            else
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    __value = new ZergRush.Samples.OtherData();
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            self[__pair.Key] = __value;
        }
    }
    public static void UpdateFrom(ref this System.Numerics.Vector3 self, System.Numerics.Vector3 other, ZRUpdateFromHelper __helper) 
    {
        self.X = other.X;
        self.Y = other.Y;
        self.Z = other.Z;
    }
    public static System.Numerics.Vector3 ReadSystem_Numerics_Vector3(this ZRBinaryReader reader) 
    {
        var self = default(System.Numerics.Vector3);
        self.X = reader.ReadSingle();
        self.Y = reader.ReadSingle();
        self.Z = reader.ReadSingle();
        return self;
    }
    public static void Serialize(this System.Numerics.Vector3 self, ZRBinaryWriter writer) 
    {
        writer.Write(self.X);
        writer.Write(self.Y);
        writer.Write(self.Z);
    }
    public static ulong CalculateHash(this System.Numerics.Vector3 self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)701202043;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.X);
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.Y);
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)BitConverter.SingleToInt32Bits(self.Z);
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static void CompareCheck(this System.Numerics.Vector3 self, System.Numerics.Vector3 other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.X != other.X) CodeGenImplTools.LogCompError(__helper, "X", printer, other.X, self.X);
        if (self.Y != other.Y) CodeGenImplTools.LogCompError(__helper, "Y", printer, other.Y, self.Y);
        if (self.Z != other.Z) CodeGenImplTools.LogCompError(__helper, "Z", printer, other.Z, self.Z);
    }
    public static System.Numerics.Vector3 ReadFromJsonSystem_Numerics_Vector3(this ZRJsonTextReader reader) 
    {
        var self = default(System.Numerics.Vector3);
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "X":
                    self.X = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    case "Y":
                    self.Y = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    case "Z":
                    self.Z = CodeGenImplTools.ReadJsonFloat(reader);
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return self;
    }
    public static void WriteJson(this System.Numerics.Vector3 self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(self.X);
        writer.WritePropertyName("Y");
        writer.WriteValue(self.Y);
        writer.WritePropertyName("Z");
        writer.WriteValue(self.Z);
        writer.WriteEndObject();
    }
    public static void UpdateFrom(this long[] self, long[] other, ZRUpdateFromHelper __helper) 
    {
        for (int i = 0; i < self.Length; i++)
        {
            self[i] = other[i];
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<long, int> self, System.Collections.Generic.Dictionary<long, int> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        long[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new long[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            self[__pair.Key] = __pair.Value;
        }
    }
    public static void UpdateFrom(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> other, ZRUpdateFromHelper __helper) 
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
                if (self[i] == null) {
                    self[i] = new ZergRush.CodeGen.Tests.SafetyEntry();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.CodeGen.Tests.SafetyEntry inst = default;
            if (other[i] == null) {
                inst = null;
            }
            else { 
                inst = new ZergRush.CodeGen.Tests.SafetyEntry();
                inst.UpdateFrom(other[i], __helper);
            }
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        long[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new long[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    if (__value == null) {
                        __value = new ZergRush.CodeGen.Tests.SafetyEntry();
                    }
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            else
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    __value = new ZergRush.CodeGen.Tests.SafetyEntry();
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            self[__pair.Key] = __value;
        }
    }
    public static long[] ReadSystem_Int64_Array(this ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<long>(size);
        var array = new long[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = reader.ReadInt64();
        }
        return array;
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<long, int> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<long>(size);
        reader.Budget.Reserve<int>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(long);
            key = reader.ReadInt64();
            var val = default(int);
            val = reader.ReadInt32();
            self.Add(key, val);
        }
    }
    public static void Deserialize(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.SafetyEntry>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            if (!reader.ReadBoolean()) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.SafetyEntry val = default;
            val = new ZergRush.CodeGen.Tests.SafetyEntry();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<long>(size);
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.SafetyEntry>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(long);
            key = reader.ReadInt64();
            if (!reader.ReadBoolean()) { self.Add(key, null); continue; }
            var val = default(ZergRush.CodeGen.Tests.SafetyEntry);
            val = new ZergRush.CodeGen.Tests.SafetyEntry();
            val.Deserialize(reader);
            self.Add(key, val);
        }
    }
    public static void Serialize(this long[] self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Length);
        for (int i = 0; i < self.Length; i++)
        {
            writer.Write(self[i]);
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<long, int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            writer.Write(item.Value);
        }
    }
    public static void Serialize(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            if (!(item.Value != null)) writer.Write(false);
            else {
                writer.Write(true);
                item.Value.Serialize(writer);
            }
        }
    }
    public static ulong CalculateHash(this long[] self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)779775932;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Length;
        for (int i = 0; i < size; i++)
        {
            hash += (ulong)self[i];
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<long, int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += (ulong)item.Key;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)item.Value;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)2032012577;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i] != null ? self[i].CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += (ulong)item.Key;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this long[] self, long[] other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Length != other.Length) CodeGenImplTools.LogCompError(__helper, "Length", printer, other.Length, self.Length);
        var count = Math.Min(self.Length, other.Length);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<long, int> self, System.Collections.Generic.Dictionary<long, int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (item.Value != otherValue) CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, otherValue, item.Value);
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static void CompareCheck(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                __helper.Push(i.ToString());
                self[i].CompareCheck(other[i], __helper, printer);
                __helper.Pop();
            }
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                if (CodeGenImplTools.CompareNull(__helper, item.Key.ToString(), printer, item.Value, otherValue)) {
                    __helper.Push(item.Key.ToString());
                    item.Value.CompareCheck(otherValue, __helper, printer);
                    __helper.Pop();
                }
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static long[] ReadFromJson(this long[] self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        var __items = new System.Collections.Generic.List<long>();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<long>(__items.Count + 1);
            long val = default;
            val = (long)(Int64)reader.Value;
            __items.Add(val);
        }
        return __items.ToArray();
    }
    public static void WriteJson(this long[] self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Length; i++)
        {
            writer.WriteValue(self[i]);
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<long, int> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<long>(self.Count + 1);
            reader.Budget.ReserveElement<int>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            long key = default;
            key = (long)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            int val = default;
            val = (int)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<long, int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            writer.WriteValue(item.Value);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.SafetyEntry>(self.Count + 1);
            if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
            ZergRush.CodeGen.Tests.SafetyEntry val = default;
            val = new ZergRush.CodeGen.Tests.SafetyEntry();
            val.ReadFromJson(reader);
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this SimpleList<ZergRush.CodeGen.Tests.SafetyEntry> self, ZRJsonTextWriter writer) 
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
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<long>(self.Count + 1);
            reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.SafetyEntry>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            long key = default;
            key = (long)(Int64)reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            ZergRush.CodeGen.Tests.SafetyEntry val = default;
            if (reader.TokenType == JsonToken.Null) {
                val = null;
            }
            else { 
                val = new ZergRush.CodeGen.Tests.SafetyEntry();
                val.ReadFromJson(reader);
            }
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<long, ZergRush.CodeGen.Tests.SafetyEntry> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            if (!(item.Value != null))
            {
                writer.WriteNull();
            }
            else
            {
                item.Value.WriteJson(writer);
            }
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
    public static void UpdateFrom(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> other, ZRUpdateFromHelper __helper) 
    {
        self.__update_mod = true;
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            if (other[i] == null) {
                self[i] = null;
            }
            else { 
                if (self[i] == null) {
                    self[i] = new ZergRush.CodeGen.Tests.SerializableLivableLeaf();
                }
                self[i].UpdateFrom(other[i], __helper);
            }
        }
        for (; i < other.Count; ++i)
        {
            var inst = new ZergRush.CodeGen.Tests.SerializableLivableLeaf();
            self.AddCopy(inst, other[i], __helper);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
        self.__update_mod = false;
    }
    public static void Deserialize(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.CodeGen.Tests.SerializableLivableLeaf>(size);
        self.Clear();
        var __previousUpdateMode = self.__update_mod;
        self.__update_mod = true;
        try
        {
            self.Capacity = size;
            for (int i = 0; i < size; i++)
            {
                self.Add(null);
                if (!reader.ReadBoolean()) continue;
                self[self.Count - 1] = new ZergRush.CodeGen.Tests.SerializableLivableLeaf();
                self[self.Count - 1].Deserialize(reader);
            }
        }
        finally { self.__update_mod = __previousUpdateMode; }
    }
    public static void Serialize(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            if (!(self[i] != null)) writer.Write(false);
            else {
                writer.Write(true);
                self[i].Serialize(writer);
            }
        }
    }
    public static ulong CalculateHash(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)203909577;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i] != null ? self[i].CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (CodeGenImplTools.CompareNull(__helper, i.ToString(), printer, self[i], other[i])) {
                __helper.Push(i.ToString());
                self[i].CompareCheck(other[i], __helper, printer);
                __helper.Pop();
            }
        }
    }
    public static bool ReadFromJson(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        var __previousUpdateMode = self.__update_mod;
        self.__update_mod = true;
        try
        {
            while (true)
            {
                reader.ReadRequired();
                if (reader.TokenType == JsonToken.EndArray) break;
                reader.Budget.ReserveElement<ZergRush.CodeGen.Tests.SerializableLivableLeaf>(self.Count + 1);
                if (reader.TokenType == JsonToken.Null) { self.Add(null); continue; }
                self.Add(null);
                ZergRush.CodeGen.Tests.SerializableLivableLeaf val = default;
                val = new ZergRush.CodeGen.Tests.SerializableLivableLeaf();
                val.ReadFromJson(reader);
                self[self.Count - 1] = val;
            }
        }
        finally { self.__update_mod = __previousUpdateMode; }
        return true;
    }
    public static void WriteJson(this ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf> self, ZRJsonTextWriter writer) 
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
    public static void UpdateFrom(this SimpleList<int> self, SimpleList<int> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i] = other[i];
        }
        for (; i < other.Count; ++i)
        {
            var inst = other[i];
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Deserialize(this SimpleList<int> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            int val = default;
            val = reader.ReadInt32();
            self.Add(val);
        }
    }
    public static void Serialize(this SimpleList<int> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            writer.Write(self[i]);
        }
    }
    public static ulong CalculateHash(this SimpleList<int> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)2032012577;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += (ulong)self[i];
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this SimpleList<int> self, SimpleList<int> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static bool ReadFromJson(this SimpleList<int> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<int>(self.Count + 1);
            int val = default;
            val = (int)(Int64)reader.Value;
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this SimpleList<int> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            writer.WriteValue(self[i]);
        }
        writer.WriteEndArray();
    }
    public static void UpdateFrom(ref this ZergRush.Samples.CustomStruct self, ZergRush.Samples.CustomStruct other, ZRUpdateFromHelper __helper) 
    {
        self.id = other.id;
        self.name = other.name;
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.Cell<ZergRush.Samples.CustomStruct> self, ZergRush.ReactiveCore.Cell<ZergRush.Samples.CustomStruct> other, ZRUpdateFromHelper __helper) 
    {

    }
    public static void UpdateFrom(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, System.Collections.Generic.List<ZergRush.Samples.CustomStruct> other, ZRUpdateFromHelper __helper) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            var __self_i_ = self[i];
            __self_i_.UpdateFrom(other[i], __helper);
            self[i] = __self_i_;
        }
        for (; i < other.Count; ++i)
        {
            ZergRush.Samples.CustomStruct inst = default;
            inst.UpdateFrom(other[i], __helper);
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        string[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new string[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                var ____value = __value;
                ____value.UpdateFrom(__pair.Value, __helper);
                __value = ____value;
            }
            else
            {
                __value.UpdateFrom(__pair.Value, __helper);
            }
            self[__pair.Key] = __value;
        }
    }
    public static ZergRush.Samples.CustomStruct ReadZergRush_Samples_CustomStruct(this ZRBinaryReader reader) 
    {
        var self = default(ZergRush.Samples.CustomStruct);
        self.id = reader.ReadInt32();
        if (!reader.ReadBoolean()) {
            self.name = null;
        }
        else { 
            self.name = reader.ReadString();
        }
        return self;
    }
    public static void Deserialize(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<ZergRush.Samples.CustomStruct>(size);
        self.Clear();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            ZergRush.Samples.CustomStruct val = default;
            val = reader.ReadZergRush_Samples_CustomStruct();
            self.Add(val);
        }
    }
    public static void Deserialize(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, ZRBinaryReader reader) 
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<string>(size);
        reader.Budget.Reserve<ZergRush.Samples.CustomStruct>(size);
        self.Clear();
        for (int i = 0; i < size; i++)
        {
            var key = default(string);
            key = string.Empty;
            key = reader.ReadString();
            var val = default(ZergRush.Samples.CustomStruct);
            val = reader.ReadZergRush_Samples_CustomStruct();
            self.Add(key, val);
        }
    }
    public static void Serialize(this ZergRush.Samples.CustomStruct self, ZRBinaryWriter writer) 
    {
        writer.Write(self.id);
        if (!(self.name != null)) writer.Write(false);
        else {
            writer.Write(true);
            writer.Write(self.name);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            self[i].Serialize(writer);
        }
    }
    public static void Serialize(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, ZRBinaryWriter writer) 
    {
        writer.Write(self.Count);
        foreach (var item in self)
        {
            writer.Write(item.Key);
            item.Value.Serialize(writer);
        }
    }
    public static ulong CalculateHash(this ZergRush.Samples.CustomStruct self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)1582263992;
        hash += hash << 11; hash ^= hash >> 7;
        hash += (ulong)self.id;
        hash += hash << 11; hash ^= hash >> 7;
        hash += self.name != null ? CodeGenImplTools.CalculateStringHash(self.name) : 345093625;
        hash += hash << 11; hash ^= hash >> 7;
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)359440710;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Count;
        for (int i = 0; i < size; i++)
        {
            hash += self[i].CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static ulong CalculateHash(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, ZRHashHelper __helper) 
    {
        ulong hash = 345093625;
        hash ^= (ulong)639239793;
        hash += hash << 11; hash ^= hash >> 7;
        foreach (var item in self)
        {
            hash += item.Key != null ? CodeGenImplTools.CalculateStringHash(item.Key) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += item.Value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(this ZergRush.Samples.CustomStruct self, ZergRush.Samples.CustomStruct other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.id != other.id) CodeGenImplTools.LogCompError(__helper, "id", printer, other.id, self.id);
        if (self.name != other.name) CodeGenImplTools.LogCompError(__helper, "name", printer, other.name, self.name);
    }
    public static void CompareCheck(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, System.Collections.Generic.List<ZergRush.Samples.CustomStruct> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        var count = Math.Min(self.Count, other.Count);
        for (int i = 0; i < count; i++)
        {
            __helper.Push(i.ToString());
            self[i].CompareCheck(other[i], __helper, printer);
            __helper.Pop();
        }
    }
    public static void CompareCheck(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> other, ZRCompareCheckHelper __helper, Action<string> printer) 
    {
        if (self.Count != other.Count) CodeGenImplTools.LogCompError(__helper, "Count", printer, other.Count, self.Count);
        foreach (var item in self)
        {
            if (!other.TryGetValue(item.Key, out var otherValue))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)"missing", (object)item.Value);
            }
            else
            {
                __helper.Push(item.Key.ToString());
                item.Value.CompareCheck(otherValue, __helper, printer);
                __helper.Pop();
            }
        }
        foreach (var item in other)
        {
            if (!self.ContainsKey(item.Key))
            {
                CodeGenImplTools.LogCompError(__helper, item.Key.ToString(), printer, (object)item.Value, (object)"missing");
            }
        }
    }
    public static ZergRush.Samples.CustomStruct ReadFromJsonZergRush_Samples_CustomStruct(this ZRJsonTextReader reader) 
    {
        var self = default(ZergRush.Samples.CustomStruct);
        reader.RequireToken(JsonToken.StartObject);
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var __name = (string) reader.Value;
                reader.ReadRequired();
                switch(__name)
                {
                    case "id":
                    self.id = (int)(Int64)reader.Value;
                    break;
                    case "name":
                    if (reader.TokenType == JsonToken.Null) {
                        self.name = null;
                    }
                    else { 
                        self.name = (string) reader.Value;
                    }
                    break;
                    default: reader.SkipObj(); break;
                }
            }
            else if (reader.TokenType == JsonToken.EndObject) { break; }
            else throw new JsonSerializationException("Expected object property.");
        }
        return self;
    }
    public static void WriteJson(this ZergRush.Samples.CustomStruct self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");
        writer.WriteValue(self.id);
        if (!(self.name != null))
        {
            writer.WritePropertyName("name");
            writer.WriteNull();
        }
        else
        {
            writer.WritePropertyName("name");
            writer.WriteValue(self.name);
        }
        writer.WriteEndObject();
    }
    public static bool ReadFromJson(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) break;
            reader.Budget.ReserveElement<ZergRush.Samples.CustomStruct>(self.Count + 1);
            ZergRush.Samples.CustomStruct val = default;
            val = (ZergRush.Samples.CustomStruct)reader.ReadFromJsonZergRush_Samples_CustomStruct();
            self.Add(val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.List<ZergRush.Samples.CustomStruct> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Count; i++)
        {
            self[i].WriteJson(writer);
        }
        writer.WriteEndArray();
    }
    public static bool ReadFromJson(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, ZRJsonTextReader reader) 
    {
        if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException("Bad Json Format");
        self.Clear();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) { break; }
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Bad Json Format");
            reader.Budget.ReserveElement<string>(self.Count + 1);
            reader.Budget.ReserveElement<ZergRush.Samples.CustomStruct>(self.Count + 1);
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "key") throw new JsonSerializationException("Expected dictionary key.");
            reader.ReadRequired();
            string key = default;
            key = string.Empty;
            key = (string) reader.Value;
            reader.ReadRequired();
            reader.RequireToken(JsonToken.PropertyName);
            if ((string)reader.Value != "value") throw new JsonSerializationException("Expected dictionary value.");
            reader.ReadRequired();
            ZergRush.Samples.CustomStruct val = default;
            val = (ZergRush.Samples.CustomStruct)reader.ReadFromJsonZergRush_Samples_CustomStruct();
            reader.ReadRequired();
            reader.RequireToken(JsonToken.EndObject);
            self.Add(key, val);
        }
        return true;
    }
    public static void WriteJson(this System.Collections.Generic.Dictionary<string, ZergRush.Samples.CustomStruct> self, ZRJsonTextWriter writer) 
    {
        writer.WriteStartArray();
        foreach (var item in self)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("key");
            writer.WriteValue(item.Key);
            writer.WritePropertyName("value");
            item.Value.WriteJson(writer);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
    }
}
#endif

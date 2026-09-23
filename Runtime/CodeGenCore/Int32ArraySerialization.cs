using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZergRush.Alive;
using ZergRush;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION

namespace ZergRush
{
public static class Int32ArraySerialization
{
    public static void UpdateFrom(int[] self, int[] other, ZRUpdateFromHelper __helper)
    {
        for (int i = 0; i < self.Length; i++)
        {
            self[i] = other[i];
        }
    }
    public static int[] ReadSystem_Int32_Array(ZRBinaryReader reader)
    {
        var size = reader.ReadInt32();
        reader.Budget.Reserve<int>(size);
        var array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = reader.ReadInt32();
        }
        return array;
    }
    public static void Serialize(int[] self, ZRBinaryWriter writer)
    {
        writer.Write(self.Length);
        for (int i = 0; i < self.Length; i++)
        {
            writer.Write(self[i]);
        }
    }
    public static ulong CalculateHash(int[] self, ZRHashHelper __helper)
    {
        ulong hash = 345093625;
        hash ^= (ulong)677530667;
        hash += hash << 11; hash ^= hash >> 7;
        var size = self.Length;
        for (int i = 0; i < size; i++)
        {
            hash += (ulong)self[i];
            hash += hash << 11; hash ^= hash >> 7;
        }
        return hash;
    }
    public static void CompareCheck(int[] self, int[] other, ZRCompareCheckHelper __helper, Action<string> printer)
    {
        if (self.Length != other.Length) CodeGenImplTools.LogCompError(__helper, "Length", printer, other.Length, self.Length);
        var count = Math.Min(self.Length, other.Length);
        for (int i = 0; i < count; i++)
        {
            if (self[i] != other[i]) CodeGenImplTools.LogCompError(__helper, i.ToString(), printer, other[i], self[i]);
        }
    }
    public static int[] ReadFromJson(int[] self, ZRJsonTextReader reader)
    {
        reader.RequireToken(JsonToken.StartArray);
        var items = new List<int>();
        while (true)
        {
            reader.ReadRequired();
            if (reader.TokenType == JsonToken.EndArray) return items.ToArray();
            reader.Budget.ReserveElement<int>(items.Count + 1);
            reader.RequireToken(JsonToken.Integer);
            items.Add(checked((int)(long)reader.Value));
        }
    }
    public static void WriteJson(int[] self, ZRJsonTextWriter writer)
    {
        writer.WriteStartArray();
        for (int i = 0; i < self.Length; i++)
        {
            writer.WriteValue(self[i]);
        }
        writer.WriteEndArray();
    }
}
}
#endif

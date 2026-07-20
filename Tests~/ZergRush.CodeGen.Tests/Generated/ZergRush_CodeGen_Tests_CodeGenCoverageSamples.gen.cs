using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class CodeGenCoverageSamples : IUpdatableFrom<ZergRush.CodeGen.Tests.CodeGenCoverageSamples>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.CodeGenCoverageSamples>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.CodeGenCoverageSamples other, ZRUpdateFromHelper __helper) 
        {
            booleanValue = other.booleanValue;
            byteValue = other.byteValue;
            charValue = other.charValue;
            decimalValue = other.decimalValue;
            doubleValue = other.doubleValue;
            enumValue = other.enumValue;
            floatValue = other.floatValue;
            intValue = other.intValue;
            longValue = other.longValue;
            nestedDictionary.UpdateFrom(other.nestedDictionary, __helper);
            nestedList.UpdateFrom(other.nestedList, __helper);
            nestedNullableCell.value.value.value = other.nestedNullableCell.value.value.value;
            nullableCell.value = other.nullableCell.value;
            nullableEnumValue = other.nullableEnumValue;
            nullableIntValue = other.nullableIntValue;
            if (other.nullableObject == null) {
                nullableObject = null;
            }
            else { 
                if (nullableObject == null) {
                    nullableObject = new ZergRush.Samples.OtherData();
                }
                nullableObject.UpdateFrom(other.nullableObject, __helper);
            }
            if (other.nullablePlainStruct == null) {
                nullablePlainStruct = null;
            }
            else {
                var __nullablePlainStruct = nullablePlainStruct.GetValueOrDefault();
                __nullablePlainStruct.UpdateFrom(other.nullablePlainStruct.Value, __helper);
                nullablePlainStruct = __nullablePlainStruct;
            }
            nullablePrimitiveDictionary.UpdateFrom(other.nullablePrimitiveDictionary, __helper);
            nullablePrimitiveList.UpdateFrom(other.nullablePrimitiveList, __helper);
            nullableString = other.nullableString;
            if (other.nullableTaggedStruct == null) {
                nullableTaggedStruct = null;
            }
            else {
                var __nullableTaggedStruct = nullableTaggedStruct.GetValueOrDefault();
                __nullableTaggedStruct.UpdateFrom(other.nullableTaggedStruct.Value, __helper);
                nullableTaggedStruct = __nullableTaggedStruct;
            }
            var __objectCell_value = objectCell.value;
            __objectCell_value.UpdateFrom(other.objectCell.value, __helper);
            objectCell.value = __objectCell_value;
            objectDictionary.UpdateFrom(other.objectDictionary, __helper);
            objectList.UpdateFrom(other.objectList, __helper);
            plainStruct.UpdateFrom(other.plainStruct, __helper);
            var primitiveArrayCount = other.primitiveArray.Length;
            var primitiveArrayTemp = primitiveArray;
            Array.Resize(ref primitiveArrayTemp, primitiveArrayCount);
            primitiveArray = primitiveArrayTemp;
            primitiveArray.UpdateFrom(other.primitiveArray, __helper);
            primitiveDictionary.UpdateFrom(other.primitiveDictionary, __helper);
            primitiveList.UpdateFrom(other.primitiveList, __helper);
            reactiveCollection.UpdateFrom(other.reactiveCollection, __helper);
            requiredObject.UpdateFrom(other.requiredObject, __helper);
            requiredString = other.requiredString;
            shortValue = other.shortValue;
            taggedStruct.UpdateFrom(other.taggedStruct, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            booleanValue = reader.ReadBoolean();
            byteValue = reader.ReadByte();
            charValue = reader.ReadChar();
            decimalValue = reader.ReadDecimal();
            doubleValue = reader.ReadDouble();
            enumValue = reader.ReadEnum<ZergRush.Samples.SampleEnum>();
            floatValue = reader.ReadSingle();
            intValue = reader.ReadInt32();
            longValue = reader.ReadInt64();
            nestedDictionary.Deserialize(reader);
            nestedList.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                nestedNullableCell.value.value.value = null;
            }
            else {
                var __nestedNullableCell_value_value_value = nestedNullableCell.value.value.value.GetValueOrDefault();
                __nestedNullableCell_value_value_value = reader.ReadInt32();
                nestedNullableCell.value.value.value = __nestedNullableCell_value_value_value;
            }
            if (!reader.ReadBoolean()) {
                nullableCell.value = null;
            }
            else {
                var __nullableCell_value = nullableCell.value.GetValueOrDefault();
                __nullableCell_value = reader.ReadInt32();
                nullableCell.value = __nullableCell_value;
            }
            if (!reader.ReadBoolean()) {
                nullableEnumValue = null;
            }
            else {
                var __nullableEnumValue = nullableEnumValue.GetValueOrDefault();
                __nullableEnumValue = reader.ReadEnum<ZergRush.Samples.SampleEnum>();
                nullableEnumValue = __nullableEnumValue;
            }
            if (!reader.ReadBoolean()) {
                nullableIntValue = null;
            }
            else {
                var __nullableIntValue = nullableIntValue.GetValueOrDefault();
                __nullableIntValue = reader.ReadInt32();
                nullableIntValue = __nullableIntValue;
            }
            if (!reader.ReadBoolean()) {
                nullableObject = null;
            }
            else { 
                if (nullableObject == null) {
                    nullableObject = new ZergRush.Samples.OtherData();
                }
                nullableObject.Deserialize(reader);
            }
            if (!reader.ReadBoolean()) {
                nullablePlainStruct = null;
            }
            else {
                var __nullablePlainStruct = nullablePlainStruct.GetValueOrDefault();
                __nullablePlainStruct = reader.ReadZergRush_Samples_PlainStruct();
                nullablePlainStruct = __nullablePlainStruct;
            }
            nullablePrimitiveDictionary.Deserialize(reader);
            nullablePrimitiveList.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                nullableString = null;
            }
            else { 
                nullableString = reader.ReadString();
            }
            if (!reader.ReadBoolean()) {
                nullableTaggedStruct = null;
            }
            else {
                var __nullableTaggedStruct = nullableTaggedStruct.GetValueOrDefault();
                __nullableTaggedStruct.Deserialize(reader);
                nullableTaggedStruct = __nullableTaggedStruct;
            }
            objectCell.value.Deserialize(reader);
            objectDictionary.Deserialize(reader);
            objectList.Deserialize(reader);
            plainStruct = reader.ReadZergRush_Samples_PlainStruct();
            primitiveArray = reader.ReadSystem_Int32_Array();
            primitiveDictionary.Deserialize(reader);
            primitiveList.Deserialize(reader);
            reactiveCollection.Deserialize(reader);
            requiredObject.Deserialize(reader);
            requiredString = reader.ReadString();
            shortValue = reader.ReadInt16();
            taggedStruct.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(booleanValue);
            writer.Write(byteValue);
            writer.Write(charValue);
            writer.Write(decimalValue);
            writer.Write(doubleValue);
            writer.Write((Int32)enumValue);
            writer.Write(floatValue);
            writer.Write(intValue);
            writer.Write(longValue);
            nestedDictionary.Serialize(writer);
            nestedList.Serialize(writer);
            if (!(nestedNullableCell.value.value.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nestedNullableCell.value.value.value.Value);
            }
            if (!(nullableCell.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nullableCell.value.Value);
            }
            if (!(nullableEnumValue.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write((Int32)nullableEnumValue.Value);
            }
            if (!(nullableIntValue.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nullableIntValue.Value);
            }
            if (!(nullableObject != null)) writer.Write(false);
            else {
                writer.Write(true);
                nullableObject.Serialize(writer);
            }
            if (!(nullablePlainStruct.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                nullablePlainStruct.Value.Serialize(writer);
            }
            nullablePrimitiveDictionary.Serialize(writer);
            nullablePrimitiveList.Serialize(writer);
            if (!(nullableString != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nullableString);
            }
            if (!(nullableTaggedStruct.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                nullableTaggedStruct.Value.Serialize(writer);
            }
            objectCell.value.Serialize(writer);
            objectDictionary.Serialize(writer);
            objectList.Serialize(writer);
            plainStruct.Serialize(writer);
            primitiveArray.Serialize(writer);
            primitiveDictionary.Serialize(writer);
            primitiveList.Serialize(writer);
            reactiveCollection.Serialize(writer);
            requiredObject.Serialize(writer);
            writer.Write(requiredString);
            writer.Write(shortValue);
            taggedStruct.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1896027210;
            hash += hash << 11; hash ^= hash >> 7;
            hash += booleanValue ? 1u : 0u;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)byteValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)charValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)decimalValue.GetHashCode();
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)BitConverter.DoubleToInt64Bits(doubleValue);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)enumValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)BitConverter.SingleToInt32Bits(floatValue);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)intValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)longValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nestedDictionary.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nestedList.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nestedNullableCell.value.value.value.HasValue ? (ulong)nestedNullableCell.value.value.value.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableCell.value.HasValue ? (ulong)nullableCell.value.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableEnumValue.HasValue ? (ulong)nullableEnumValue.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableIntValue.HasValue ? (ulong)nullableIntValue.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableObject != null ? nullableObject.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullablePlainStruct.HasValue ? nullablePlainStruct.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullablePrimitiveDictionary.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullablePrimitiveList.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableString != null ? CodeGenImplTools.CalculateStringHash(nullableString) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableTaggedStruct.HasValue ? nullableTaggedStruct.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += objectCell.value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += objectDictionary.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += objectList.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += plainStruct.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += primitiveArray.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += primitiveDictionary.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += primitiveList.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveCollection.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += requiredObject.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(requiredString);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)shortValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += taggedStruct.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  CodeGenCoverageSamples() 
        {
            nestedDictionary = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>>();
            nestedList = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
            nestedNullableCell = new ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<int?>>>();
            nestedNullableCell.value = new ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<int?>>();
            nestedNullableCell.value.value = new ZergRush.ReactiveCore.Cell<int?>();
            nullableCell = new ZergRush.ReactiveCore.Cell<int?>();
            nullableEnumValue = new ZergRush.Samples.SampleEnum?();
            nullableIntValue = new int?();
            nullablePlainStruct = new ZergRush.Samples.PlainStruct?();
            nullablePrimitiveDictionary = new System.Collections.Generic.Dictionary<string, int?>();
            nullablePrimitiveList = new System.Collections.Generic.List<int?>();
            nullableTaggedStruct = new ZergRush.Samples.TaggedStruct?();
            objectCell = new ZergRush.ReactiveCore.Cell<ZergRush.Samples.OtherData>();
            objectCell.value = new ZergRush.Samples.OtherData();
            objectDictionary = new System.Collections.Generic.Dictionary<string, ZergRush.Samples.OtherData>();
            objectList = new System.Collections.Generic.List<ZergRush.Samples.OtherData>();
            primitiveArray = Array.Empty<int>();
            primitiveDictionary = new System.Collections.Generic.Dictionary<string, int>();
            primitiveList = new System.Collections.Generic.List<int>();
            reactiveCollection = new ZergRush.ReactiveCore.ReactiveCollection<int>();
            requiredObject = new ZergRush.Samples.OtherData();
            requiredString = string.Empty;
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.CodeGenCoverageSamples other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (booleanValue != other.booleanValue) CodeGenImplTools.LogCompError(__helper, "booleanValue", printer, other.booleanValue, booleanValue);
            if (byteValue != other.byteValue) CodeGenImplTools.LogCompError(__helper, "byteValue", printer, other.byteValue, byteValue);
            if (charValue != other.charValue) CodeGenImplTools.LogCompError(__helper, "charValue", printer, other.charValue, charValue);
            if (decimalValue != other.decimalValue) CodeGenImplTools.LogCompError(__helper, "decimalValue", printer, other.decimalValue, decimalValue);
            if (doubleValue != other.doubleValue) CodeGenImplTools.LogCompError(__helper, "doubleValue", printer, other.doubleValue, doubleValue);
            if (enumValue != other.enumValue) CodeGenImplTools.LogCompError(__helper, "enumValue", printer, other.enumValue, enumValue);
            if (floatValue != other.floatValue) CodeGenImplTools.LogCompError(__helper, "floatValue", printer, other.floatValue, floatValue);
            if (intValue != other.intValue) CodeGenImplTools.LogCompError(__helper, "intValue", printer, other.intValue, intValue);
            if (longValue != other.longValue) CodeGenImplTools.LogCompError(__helper, "longValue", printer, other.longValue, longValue);
            __helper.Push("nestedDictionary");
            nestedDictionary.CompareCheck(other.nestedDictionary, __helper, printer);
            __helper.Pop();
            __helper.Push("nestedList");
            nestedList.CompareCheck(other.nestedList, __helper, printer);
            __helper.Pop();
            if (nestedNullableCell.value.value.value != other.nestedNullableCell.value.value.value) CodeGenImplTools.LogCompError(__helper, "nestedNullableCell", printer, other.nestedNullableCell.value.value.value, nestedNullableCell.value.value.value);
            if (nullableCell.value != other.nullableCell.value) CodeGenImplTools.LogCompError(__helper, "nullableCell", printer, other.nullableCell.value, nullableCell.value);
            if (nullableEnumValue != other.nullableEnumValue) CodeGenImplTools.LogCompError(__helper, "nullableEnumValue", printer, other.nullableEnumValue, nullableEnumValue);
            if (nullableIntValue != other.nullableIntValue) CodeGenImplTools.LogCompError(__helper, "nullableIntValue", printer, other.nullableIntValue, nullableIntValue);
            if (CodeGenImplTools.CompareNull(__helper, "nullableObject", printer, nullableObject, other.nullableObject)) {
                __helper.Push("nullableObject");
                nullableObject.CompareCheck(other.nullableObject, __helper, printer);
                __helper.Pop();
            }
            if (CodeGenImplTools.CompareNullable(__helper, "nullablePlainStruct", printer, nullablePlainStruct, other.nullablePlainStruct)) {
                __helper.Push("nullablePlainStruct");
                nullablePlainStruct.Value.CompareCheck(other.nullablePlainStruct.Value, __helper, printer);
                __helper.Pop();
            }
            __helper.Push("nullablePrimitiveDictionary");
            nullablePrimitiveDictionary.CompareCheck(other.nullablePrimitiveDictionary, __helper, printer);
            __helper.Pop();
            __helper.Push("nullablePrimitiveList");
            nullablePrimitiveList.CompareCheck(other.nullablePrimitiveList, __helper, printer);
            __helper.Pop();
            if (nullableString != other.nullableString) CodeGenImplTools.LogCompError(__helper, "nullableString", printer, other.nullableString, nullableString);
            if (CodeGenImplTools.CompareNullable(__helper, "nullableTaggedStruct", printer, nullableTaggedStruct, other.nullableTaggedStruct)) {
                __helper.Push("nullableTaggedStruct");
                nullableTaggedStruct.Value.CompareCheck(other.nullableTaggedStruct.Value, __helper, printer);
                __helper.Pop();
            }
            __helper.Push("objectCell");
            objectCell.value.CompareCheck(other.objectCell.value, __helper, printer);
            __helper.Pop();
            __helper.Push("objectDictionary");
            objectDictionary.CompareCheck(other.objectDictionary, __helper, printer);
            __helper.Pop();
            __helper.Push("objectList");
            objectList.CompareCheck(other.objectList, __helper, printer);
            __helper.Pop();
            __helper.Push("plainStruct");
            plainStruct.CompareCheck(other.plainStruct, __helper, printer);
            __helper.Pop();
            __helper.Push("primitiveArray");
            primitiveArray.CompareCheck(other.primitiveArray, __helper, printer);
            __helper.Pop();
            __helper.Push("primitiveDictionary");
            primitiveDictionary.CompareCheck(other.primitiveDictionary, __helper, printer);
            __helper.Pop();
            __helper.Push("primitiveList");
            primitiveList.CompareCheck(other.primitiveList, __helper, printer);
            __helper.Pop();
            __helper.Push("reactiveCollection");
            reactiveCollection.CompareCheck(other.reactiveCollection, __helper, printer);
            __helper.Pop();
            __helper.Push("requiredObject");
            requiredObject.CompareCheck(other.requiredObject, __helper, printer);
            __helper.Pop();
            if (requiredString != other.requiredString) CodeGenImplTools.LogCompError(__helper, "requiredString", printer, other.requiredString, requiredString);
            if (shortValue != other.shortValue) CodeGenImplTools.LogCompError(__helper, "shortValue", printer, other.shortValue, shortValue);
            __helper.Push("taggedStruct");
            taggedStruct.CompareCheck(other.taggedStruct, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "booleanValue":
                booleanValue = (bool)reader.Value;
                break;
                case "byteValue":
                byteValue = (byte)(Int64)reader.Value;
                break;
                case "charValue":
                charValue = char.Parse((string)reader.Value);
                break;
                case "decimalValue":
                decimalValue = Convert.ToDecimal(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                break;
                case "doubleValue":
                doubleValue = reader.ReadJsonDouble();
                break;
                case "enumValue":
                enumValue = System.Enum.Parse<ZergRush.Samples.SampleEnum>((string)reader.Value);
                break;
                case "floatValue":
                floatValue = CodeGenImplTools.ReadJsonFloat(reader);
                break;
                case "intValue":
                intValue = (int)(Int64)reader.Value;
                break;
                case "longValue":
                longValue = (long)(Int64)reader.Value;
                break;
                case "nestedDictionary":
                nestedDictionary.ReadFromJson(reader);
                break;
                case "nestedList":
                nestedList.ReadFromJson(reader);
                break;
                case "nestedNullableCell":
                if (reader.TokenType == JsonToken.Null) {
                    nestedNullableCell.value.value.value = null;
                }
                else {
                    var __nestedNullableCell_value_value_value = nestedNullableCell.value.value.value.GetValueOrDefault();
                    __nestedNullableCell_value_value_value = (int)(Int64)reader.Value;
                    nestedNullableCell.value.value.value = __nestedNullableCell_value_value_value;
                }
                break;
                case "nullableCell":
                if (reader.TokenType == JsonToken.Null) {
                    nullableCell.value = null;
                }
                else {
                    var __nullableCell_value = nullableCell.value.GetValueOrDefault();
                    __nullableCell_value = (int)(Int64)reader.Value;
                    nullableCell.value = __nullableCell_value;
                }
                break;
                case "nullableEnumValue":
                if (reader.TokenType == JsonToken.Null) {
                    nullableEnumValue = null;
                }
                else {
                    var __nullableEnumValue = nullableEnumValue.GetValueOrDefault();
                    __nullableEnumValue = System.Enum.Parse<ZergRush.Samples.SampleEnum>((string)reader.Value);
                    nullableEnumValue = __nullableEnumValue;
                }
                break;
                case "nullableIntValue":
                if (reader.TokenType == JsonToken.Null) {
                    nullableIntValue = null;
                }
                else {
                    var __nullableIntValue = nullableIntValue.GetValueOrDefault();
                    __nullableIntValue = (int)(Int64)reader.Value;
                    nullableIntValue = __nullableIntValue;
                }
                break;
                case "nullableObject":
                if (reader.TokenType == JsonToken.Null) {
                    nullableObject = null;
                }
                else { 
                    if (nullableObject == null) {
                        nullableObject = new ZergRush.Samples.OtherData();
                    }
                    nullableObject.ReadFromJson(reader);
                }
                break;
                case "nullablePlainStruct":
                if (reader.TokenType == JsonToken.Null) {
                    nullablePlainStruct = null;
                }
                else {
                    var __nullablePlainStruct = nullablePlainStruct.GetValueOrDefault();
                    __nullablePlainStruct = (ZergRush.Samples.PlainStruct)reader.ReadFromJsonZergRush_Samples_PlainStruct();
                    nullablePlainStruct = __nullablePlainStruct;
                }
                break;
                case "nullablePrimitiveDictionary":
                nullablePrimitiveDictionary.ReadFromJson(reader);
                break;
                case "nullablePrimitiveList":
                nullablePrimitiveList.ReadFromJson(reader);
                break;
                case "nullableString":
                if (reader.TokenType == JsonToken.Null) {
                    nullableString = null;
                }
                else { 
                    nullableString = (string) reader.Value;
                }
                break;
                case "nullableTaggedStruct":
                if (reader.TokenType == JsonToken.Null) {
                    nullableTaggedStruct = null;
                }
                else {
                    var __nullableTaggedStruct = nullableTaggedStruct.GetValueOrDefault();
                    __nullableTaggedStruct.ReadFromJson(reader);
                    nullableTaggedStruct = __nullableTaggedStruct;
                }
                break;
                case "objectCell":
                objectCell.value.ReadFromJson(reader);
                break;
                case "objectDictionary":
                objectDictionary.ReadFromJson(reader);
                break;
                case "objectList":
                objectList.ReadFromJson(reader);
                break;
                case "plainStruct":
                plainStruct = (ZergRush.Samples.PlainStruct)reader.ReadFromJsonZergRush_Samples_PlainStruct();
                break;
                case "primitiveArray":
                primitiveArray = primitiveArray.ReadFromJson(reader);
                break;
                case "primitiveDictionary":
                primitiveDictionary.ReadFromJson(reader);
                break;
                case "primitiveList":
                primitiveList.ReadFromJson(reader);
                break;
                case "reactiveCollection":
                reactiveCollection.ReadFromJson(reader);
                break;
                case "requiredObject":
                requiredObject.ReadFromJson(reader);
                break;
                case "requiredString":
                requiredString = (string) reader.Value;
                break;
                case "shortValue":
                shortValue = (short)(Int64)reader.Value;
                break;
                case "taggedStruct":
                taggedStruct.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("booleanValue");
            writer.WriteValue(booleanValue);
            writer.WritePropertyName("byteValue");
            writer.WriteValue(byteValue);
            writer.WritePropertyName("charValue");
            writer.WriteValue(charValue);
            writer.WritePropertyName("decimalValue");
            writer.WriteValue(decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture));
            writer.WritePropertyName("doubleValue");
            writer.WriteValue(doubleValue);
            writer.WritePropertyName("enumValue");
            writer.WriteValue(enumValue.ToString());
            writer.WritePropertyName("floatValue");
            writer.WriteValue(floatValue);
            writer.WritePropertyName("intValue");
            writer.WriteValue(intValue);
            writer.WritePropertyName("longValue");
            writer.WriteValue(longValue);
            writer.WritePropertyName("nestedDictionary");
            nestedDictionary.WriteJson(writer);
            writer.WritePropertyName("nestedList");
            nestedList.WriteJson(writer);
            if (!(nestedNullableCell.value.value.value.HasValue))
            {
                writer.WritePropertyName("nestedNullableCell");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nestedNullableCell");
                writer.WriteValue(nestedNullableCell.value.value.value.Value);
            }
            if (!(nullableCell.value.HasValue))
            {
                writer.WritePropertyName("nullableCell");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableCell");
                writer.WriteValue(nullableCell.value.Value);
            }
            if (!(nullableEnumValue.HasValue))
            {
                writer.WritePropertyName("nullableEnumValue");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableEnumValue");
                writer.WriteValue(nullableEnumValue.Value.ToString());
            }
            if (!(nullableIntValue.HasValue))
            {
                writer.WritePropertyName("nullableIntValue");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableIntValue");
                writer.WriteValue(nullableIntValue.Value);
            }
            if (!(nullableObject != null))
            {
                writer.WritePropertyName("nullableObject");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableObject");
                nullableObject.WriteJson(writer);
            }
            if (!(nullablePlainStruct.HasValue))
            {
                writer.WritePropertyName("nullablePlainStruct");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullablePlainStruct");
                nullablePlainStruct.Value.WriteJson(writer);
            }
            writer.WritePropertyName("nullablePrimitiveDictionary");
            nullablePrimitiveDictionary.WriteJson(writer);
            writer.WritePropertyName("nullablePrimitiveList");
            nullablePrimitiveList.WriteJson(writer);
            if (!(nullableString != null))
            {
                writer.WritePropertyName("nullableString");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableString");
                writer.WriteValue(nullableString);
            }
            if (!(nullableTaggedStruct.HasValue))
            {
                writer.WritePropertyName("nullableTaggedStruct");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableTaggedStruct");
                nullableTaggedStruct.Value.WriteJson(writer);
            }
            writer.WritePropertyName("objectCell");
            objectCell.value.WriteJson(writer);
            writer.WritePropertyName("objectDictionary");
            objectDictionary.WriteJson(writer);
            writer.WritePropertyName("objectList");
            objectList.WriteJson(writer);
            writer.WritePropertyName("plainStruct");
            plainStruct.WriteJson(writer);
            writer.WritePropertyName("primitiveArray");
            primitiveArray.WriteJson(writer);
            writer.WritePropertyName("primitiveDictionary");
            primitiveDictionary.WriteJson(writer);
            writer.WritePropertyName("primitiveList");
            primitiveList.WriteJson(writer);
            writer.WritePropertyName("reactiveCollection");
            reactiveCollection.WriteJson(writer);
            writer.WritePropertyName("requiredObject");
            requiredObject.WriteJson(writer);
            writer.WritePropertyName("requiredString");
            writer.WriteValue(requiredString);
            writer.WritePropertyName("shortValue");
            writer.WriteValue(shortValue);
            writer.WritePropertyName("taggedStruct");
            taggedStruct.WriteJson(writer);
        }
    }
}
#endif

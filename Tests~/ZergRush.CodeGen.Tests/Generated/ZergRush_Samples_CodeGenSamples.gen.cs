using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.Samples {

    public partial class CodeGenSamples : IUpdatableFrom<ZergRush.Samples.CodeGenSamples>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Samples.CodeGenSamples>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            CodeGenSamples = 1,
            Ancestor = 2,
        }
        static Func<CodeGenSamples> [] polymorphConstructors = new Func<CodeGenSamples> [] {
            () => null, // 0
            () => new ZergRush.Samples.CodeGenSamples(), // 1
            () => new ZergRush.Samples.Ancestor(), // 2
        };
        public static CodeGenSamples CreatePolymorphic(ushort typeId) {
            return polymorphConstructors[typeId]();
        }
        public CodeGenSamplesType type => (CodeGenSamplesType) GetClassId();
        public virtual void UpdateFrom(ZergRush.Samples.CodeGenSamples other, ZRUpdateFromHelper __helper) 
        {
            ancestorArray.UpdateFrom(other.ancestorArray, __helper);
            var arraysAreOkCount = other.arraysAreOk.Length;
            var arraysAreOkTemp = arraysAreOk;
            Array.Resize(ref arraysAreOkTemp, arraysAreOkCount);
            arraysAreOk = arraysAreOkTemp;
            arraysAreOk.UpdateFrom(other.arraysAreOk, __helper);
            enumValue = other.enumValue;
            externalClass.UpdateFrom(other.externalClass, __helper);
            genericAncestorArray.UpdateFrom(other.genericAncestorArray, __helper);
            genericWithCustomStruct.UpdateFrom(other.genericWithCustomStruct, __helper);
            genericWithPrimitive.UpdateFrom(other.genericWithPrimitive, __helper);
            intField = other.intField;
            listOfNullablePrimitives.UpdateFrom(other.listOfNullablePrimitives, __helper);
            listsOfDataAreOk.UpdateFrom(other.listsOfDataAreOk, __helper);
            listsOfPrimitivesAreOk.UpdateFrom(other.listsOfPrimitivesAreOk, __helper);
            nestedReactiveNullablePrimitive.value.value.value = other.nestedReactiveNullablePrimitive.value.value.value;
            nullableEnumValue = other.nullableEnumValue;
            if (other.nullablePlainStruct == null) {
                nullablePlainStruct = null;
            }
            else {
                var __nullablePlainStruct = nullablePlainStruct.GetValueOrDefault();
                __nullablePlainStruct.UpdateFrom(other.nullablePlainStruct.Value, __helper);
                nullablePlainStruct = __nullablePlainStruct;
            }
            nullablePrimitive = other.nullablePrimitive;
            var __nullableReactiveValue_value = nullableReactiveValue.value;
            if (other.nullableReactiveValue.value == null) {
                __nullableReactiveValue_value = null;
            }
            else { 
                if (__nullableReactiveValue_value == null) {
                    __nullableReactiveValue_value = new ZergRush.Samples.OtherData();
                }
                __nullableReactiveValue_value.UpdateFrom(other.nullableReactiveValue.value, __helper);
            }
            nullableReactiveValue.value = __nullableReactiveValue_value;
            if (other.nullableStruct == null) {
                nullableStruct = null;
            }
            else {
                var __nullableStruct = nullableStruct.GetValueOrDefault();
                __nullableStruct.UpdateFrom(other.nullableStruct.Value, __helper);
                nullableStruct = __nullableStruct;
            }
            if (other.nullableStructWithGenerationTags == null) {
                nullableStructWithGenerationTags = null;
            }
            else {
                var __nullableStructWithGenerationTags = nullableStructWithGenerationTags.GetValueOrDefault();
                __nullableStructWithGenerationTags.UpdateFrom(other.nullableStructWithGenerationTags.Value, __helper);
                nullableStructWithGenerationTags = __nullableStructWithGenerationTags;
            }
            if (other.otherData == null) {
                otherData = null;
            }
            else { 
                if (otherData == null) {
                    otherData = new ZergRush.Samples.OtherData();
                }
                otherData.UpdateFrom(other.otherData, __helper);
            }
            otherData2.UpdateFrom(other.otherData2, __helper);
            reactiveCollections.UpdateFrom(other.reactiveCollections, __helper);
            reactiveNullablePrimitive.value = other.reactiveNullablePrimitive.value;
            if (other.reactiveNullableStruct.value == null) {
                reactiveNullableStruct.value = null;
            }
            else {
                var __reactiveNullableStruct_value = reactiveNullableStruct.value.GetValueOrDefault();
                __reactiveNullableStruct_value.UpdateFrom(other.reactiveNullableStruct.value.Value, __helper);
                reactiveNullableStruct.value = __reactiveNullableStruct_value;
            }
            if (other.reactiveNullableStructWithGenerationTags.value == null) {
                reactiveNullableStructWithGenerationTags.value = null;
            }
            else {
                var __reactiveNullableStructWithGenerationTags_value = reactiveNullableStructWithGenerationTags.value.GetValueOrDefault();
                __reactiveNullableStructWithGenerationTags_value.UpdateFrom(other.reactiveNullableStructWithGenerationTags.value.Value, __helper);
                reactiveNullableStructWithGenerationTags.value = __reactiveNullableStructWithGenerationTags_value;
            }
            var __reactiveValue_value = reactiveValue.value;
            __reactiveValue_value.UpdateFrom(other.reactiveValue.value, __helper);
            reactiveValue.value = __reactiveValue_value;
            stringFieldMustNotBeNull = other.stringFieldMustNotBeNull;
            stringFieldThatCanBeNull = other.stringFieldThatCanBeNull;
            stringProp = other.stringProp;
            vector.UpdateFrom(other.vector, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            ancestorArray.Deserialize(reader);
            arraysAreOk = reader.ReadSystem_Int32_Array();
            complexStructuresAreAlsoOk.Deserialize(reader);
            dictsAreOk.Deserialize(reader);
            dictWithNullableValues.Deserialize(reader);
            enumValue = reader.ReadEnum<ZergRush.Samples.SampleEnum>();
            externalClass.Deserialize(reader);
            genericAncestorArray.Deserialize(reader);
            genericWithCustomStruct.Deserialize(reader);
            genericWithPrimitive.Deserialize(reader);
            intField = reader.ReadInt32();
            listOfNullablePrimitives.Deserialize(reader);
            listsOfDataAreOk.Deserialize(reader);
            listsOfPrimitivesAreOk.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                nestedReactiveNullablePrimitive.value.value.value = null;
            }
            else {
                var __nestedReactiveNullablePrimitive_value_value_value = nestedReactiveNullablePrimitive.value.value.value.GetValueOrDefault();
                __nestedReactiveNullablePrimitive_value_value_value = reader.ReadInt32();
                nestedReactiveNullablePrimitive.value.value.value = __nestedReactiveNullablePrimitive_value_value_value;
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
                nullablePlainStruct = null;
            }
            else {
                var __nullablePlainStruct = nullablePlainStruct.GetValueOrDefault();
                __nullablePlainStruct = reader.ReadZergRush_Samples_PlainStruct();
                nullablePlainStruct = __nullablePlainStruct;
            }
            if (!reader.ReadBoolean()) {
                nullablePrimitive = null;
            }
            else {
                var __nullablePrimitive = nullablePrimitive.GetValueOrDefault();
                __nullablePrimitive = reader.ReadInt32();
                nullablePrimitive = __nullablePrimitive;
            }
            if (!reader.ReadBoolean()) {
                nullableReactiveValue.value = null;
            }
            else { 
                if (nullableReactiveValue.value == null) {
                    nullableReactiveValue.value = new ZergRush.Samples.OtherData();
                }
                nullableReactiveValue.value.Deserialize(reader);
            }
            if (!reader.ReadBoolean()) {
                nullableStruct = null;
            }
            else {
                var __nullableStruct = nullableStruct.GetValueOrDefault();
                __nullableStruct = reader.ReadUnityEngine_Vector3();
                nullableStruct = __nullableStruct;
            }
            if (!reader.ReadBoolean()) {
                nullableStructWithGenerationTags = null;
            }
            else {
                var __nullableStructWithGenerationTags = nullableStructWithGenerationTags.GetValueOrDefault();
                __nullableStructWithGenerationTags.Deserialize(reader);
                nullableStructWithGenerationTags = __nullableStructWithGenerationTags;
            }
            if (!reader.ReadBoolean()) {
                otherData = null;
            }
            else { 
                if (otherData == null) {
                    otherData = new ZergRush.Samples.OtherData();
                }
                otherData.Deserialize(reader);
            }
            otherData2.Deserialize(reader);
            reactiveCollections.Deserialize(reader);
            if (!reader.ReadBoolean()) {
                reactiveNullablePrimitive.value = null;
            }
            else {
                var __reactiveNullablePrimitive_value = reactiveNullablePrimitive.value.GetValueOrDefault();
                __reactiveNullablePrimitive_value = reader.ReadInt32();
                reactiveNullablePrimitive.value = __reactiveNullablePrimitive_value;
            }
            if (!reader.ReadBoolean()) {
                reactiveNullableStruct.value = null;
            }
            else {
                var __reactiveNullableStruct_value = reactiveNullableStruct.value.GetValueOrDefault();
                __reactiveNullableStruct_value = reader.ReadUnityEngine_Vector3();
                reactiveNullableStruct.value = __reactiveNullableStruct_value;
            }
            if (!reader.ReadBoolean()) {
                reactiveNullableStructWithGenerationTags.value = null;
            }
            else {
                var __reactiveNullableStructWithGenerationTags_value = reactiveNullableStructWithGenerationTags.value.GetValueOrDefault();
                __reactiveNullableStructWithGenerationTags_value.Deserialize(reader);
                reactiveNullableStructWithGenerationTags.value = __reactiveNullableStructWithGenerationTags_value;
            }
            reactiveValue.value.Deserialize(reader);
            stringFieldMustNotBeNull = reader.ReadString();
            if (!reader.ReadBoolean()) {
                stringFieldThatCanBeNull = null;
            }
            else { 
                stringFieldThatCanBeNull = reader.ReadString();
            }
            stringProp = reader.ReadString();
            vector = reader.ReadUnityEngine_Vector3();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            ancestorArray.Serialize(writer);
            arraysAreOk.Serialize(writer);
            complexStructuresAreAlsoOk.Serialize(writer);
            dictsAreOk.Serialize(writer);
            dictWithNullableValues.Serialize(writer);
            writer.Write((Int32)enumValue);
            externalClass.Serialize(writer);
            genericAncestorArray.Serialize(writer);
            genericWithCustomStruct.Serialize(writer);
            genericWithPrimitive.Serialize(writer);
            writer.Write(intField);
            listOfNullablePrimitives.Serialize(writer);
            listsOfDataAreOk.Serialize(writer);
            listsOfPrimitivesAreOk.Serialize(writer);
            if (!(nestedReactiveNullablePrimitive.value.value.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nestedReactiveNullablePrimitive.value.value.value.Value);
            }
            if (!(nullableEnumValue.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write((Int32)nullableEnumValue.Value);
            }
            if (!(nullablePlainStruct.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                nullablePlainStruct.Value.Serialize(writer);
            }
            if (!(nullablePrimitive.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(nullablePrimitive.Value);
            }
            if (!(nullableReactiveValue.value != null)) writer.Write(false);
            else {
                writer.Write(true);
                nullableReactiveValue.value.Serialize(writer);
            }
            if (!(nullableStruct.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                nullableStruct.Value.Serialize(writer);
            }
            if (!(nullableStructWithGenerationTags.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                nullableStructWithGenerationTags.Value.Serialize(writer);
            }
            if (!(otherData != null)) writer.Write(false);
            else {
                writer.Write(true);
                otherData.Serialize(writer);
            }
            otherData2.Serialize(writer);
            reactiveCollections.Serialize(writer);
            if (!(reactiveNullablePrimitive.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(reactiveNullablePrimitive.value.Value);
            }
            if (!(reactiveNullableStruct.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                reactiveNullableStruct.value.Value.Serialize(writer);
            }
            if (!(reactiveNullableStructWithGenerationTags.value.HasValue)) writer.Write(false);
            else {
                writer.Write(true);
                reactiveNullableStructWithGenerationTags.value.Value.Serialize(writer);
            }
            reactiveValue.value.Serialize(writer);
            writer.Write(stringFieldMustNotBeNull);
            if (!(stringFieldThatCanBeNull != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(stringFieldThatCanBeNull);
            }
            writer.Write(stringProp);
            vector.Serialize(writer);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)2069206495;
            hash += hash << 11; hash ^= hash >> 7;
            hash += ancestorArray.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += arraysAreOk.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += complexStructuresAreAlsoOk.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += dictsAreOk.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += dictWithNullableValues.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)enumValue;
            hash += hash << 11; hash ^= hash >> 7;
            hash += externalClass.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += genericAncestorArray.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += genericWithCustomStruct.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += genericWithPrimitive.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)intField;
            hash += hash << 11; hash ^= hash >> 7;
            hash += listOfNullablePrimitives.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += listsOfDataAreOk.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += listsOfPrimitivesAreOk.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += nestedReactiveNullablePrimitive.value.value.value.HasValue ? (ulong)nestedReactiveNullablePrimitive.value.value.value.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableEnumValue.HasValue ? (ulong)nullableEnumValue.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullablePlainStruct.HasValue ? nullablePlainStruct.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullablePrimitive.HasValue ? (ulong)nullablePrimitive.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableReactiveValue.value != null ? nullableReactiveValue.value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableStruct.HasValue ? nullableStruct.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += nullableStructWithGenerationTags.HasValue ? nullableStructWithGenerationTags.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += otherData != null ? otherData.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += otherData2.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveCollections.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveNullablePrimitive.value.HasValue ? (ulong)reactiveNullablePrimitive.value.Value : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveNullableStruct.value.HasValue ? reactiveNullableStruct.value.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveNullableStructWithGenerationTags.value.HasValue ? reactiveNullableStructWithGenerationTags.value.Value.CalculateHash(__helper) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += reactiveValue.value.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(stringFieldMustNotBeNull);
            hash += hash << 11; hash ^= hash >> 7;
            hash += stringFieldThatCanBeNull != null ? CodeGenImplTools.CalculateStringHash(stringFieldThatCanBeNull) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(stringProp);
            hash += hash << 11; hash ^= hash >> 7;
            hash += vector.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  CodeGenSamples() 
        {
            arraysAreOk = Array.Empty<int>();
            complexStructuresAreAlsoOk = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.List<string>>>();
            dictsAreOk = new System.Collections.Generic.Dictionary<int, ZergRush.Samples.OtherData>();
            dictWithNullableValues = new System.Collections.Generic.Dictionary<string, int?>();
            externalClass = new ZergRush.Samples.ExternalClass();
            genericAncestorArray = new System.Collections.Generic.List<ZergRush.Samples.TestPolyGenericParent>();
            genericWithCustomStruct = new ZergRush.Samples.TestGeneric<ZergRush.Samples.CustomStruct>();
            genericWithPrimitive = new ZergRush.Samples.TestGeneric<int>();
            listOfNullablePrimitives = new System.Collections.Generic.List<int?>();
            listsOfDataAreOk = new System.Collections.Generic.List<ZergRush.Samples.OtherData>();
            listsOfPrimitivesAreOk = new System.Collections.Generic.List<int>();
            nestedReactiveNullablePrimitive = new ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<int?>>>();
            nestedReactiveNullablePrimitive.value = new ZergRush.ReactiveCore.Cell<ZergRush.ReactiveCore.Cell<int?>>();
            nestedReactiveNullablePrimitive.value.value = new ZergRush.ReactiveCore.Cell<int?>();
            nullableEnumValue = new ZergRush.Samples.SampleEnum?();
            nullablePlainStruct = new ZergRush.Samples.PlainStruct?();
            nullablePrimitive = new int?();
            nullableReactiveValue = new ZergRush.ReactiveCore.Cell<ZergRush.Samples.OtherData>();
            nullableStruct = new UnityEngine.Vector3?();
            nullableStructWithGenerationTags = new ZergRush.Samples.TaggedStruct?();
            reactiveCollections = new ZergRush.ReactiveCore.ReactiveCollection<int>();
            reactiveNullablePrimitive = new ZergRush.ReactiveCore.Cell<int?>();
            reactiveNullableStruct = new ZergRush.ReactiveCore.Cell<UnityEngine.Vector3?>();
            reactiveNullableStructWithGenerationTags = new ZergRush.ReactiveCore.Cell<ZergRush.Samples.TaggedStruct?>();
            reactiveValue = new ZergRush.ReactiveCore.Cell<ZergRush.Samples.OtherData>();
            reactiveValue.value = new ZergRush.Samples.OtherData();
            stringFieldMustNotBeNull = string.Empty;
            stringProp = string.Empty;
        }
        public virtual void CompareCheck(ZergRush.Samples.CodeGenSamples other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            __helper.Push("ancestorArray");
            ancestorArray.CompareCheck(other.ancestorArray, __helper, printer);
            __helper.Pop();
            __helper.Push("arraysAreOk");
            arraysAreOk.CompareCheck(other.arraysAreOk, __helper, printer);
            __helper.Pop();
            __helper.Push("complexStructuresAreAlsoOk");
            complexStructuresAreAlsoOk.CompareCheck(other.complexStructuresAreAlsoOk, __helper, printer);
            __helper.Pop();
            __helper.Push("dictsAreOk");
            dictsAreOk.CompareCheck(other.dictsAreOk, __helper, printer);
            __helper.Pop();
            __helper.Push("dictWithNullableValues");
            dictWithNullableValues.CompareCheck(other.dictWithNullableValues, __helper, printer);
            __helper.Pop();
            if (enumValue != other.enumValue) CodeGenImplTools.LogCompError(__helper, "enumValue", printer, other.enumValue, enumValue);
            __helper.Push("externalClass");
            externalClass.CompareCheck(other.externalClass, __helper, printer);
            __helper.Pop();
            __helper.Push("genericAncestorArray");
            genericAncestorArray.CompareCheck(other.genericAncestorArray, __helper, printer);
            __helper.Pop();
            __helper.Push("genericWithCustomStruct");
            genericWithCustomStruct.CompareCheck(other.genericWithCustomStruct, __helper, printer);
            __helper.Pop();
            __helper.Push("genericWithPrimitive");
            genericWithPrimitive.CompareCheck(other.genericWithPrimitive, __helper, printer);
            __helper.Pop();
            if (intField != other.intField) CodeGenImplTools.LogCompError(__helper, "intField", printer, other.intField, intField);
            __helper.Push("listOfNullablePrimitives");
            listOfNullablePrimitives.CompareCheck(other.listOfNullablePrimitives, __helper, printer);
            __helper.Pop();
            __helper.Push("listsOfDataAreOk");
            listsOfDataAreOk.CompareCheck(other.listsOfDataAreOk, __helper, printer);
            __helper.Pop();
            __helper.Push("listsOfPrimitivesAreOk");
            listsOfPrimitivesAreOk.CompareCheck(other.listsOfPrimitivesAreOk, __helper, printer);
            __helper.Pop();
            if (nestedReactiveNullablePrimitive.value.value.value != other.nestedReactiveNullablePrimitive.value.value.value) CodeGenImplTools.LogCompError(__helper, "nestedReactiveNullablePrimitive", printer, other.nestedReactiveNullablePrimitive.value.value.value, nestedReactiveNullablePrimitive.value.value.value);
            if (nullableEnumValue != other.nullableEnumValue) CodeGenImplTools.LogCompError(__helper, "nullableEnumValue", printer, other.nullableEnumValue, nullableEnumValue);
            if (CodeGenImplTools.CompareNullable(__helper, "nullablePlainStruct", printer, nullablePlainStruct, other.nullablePlainStruct)) {
                __helper.Push("nullablePlainStruct");
                nullablePlainStruct.Value.CompareCheck(other.nullablePlainStruct.Value, __helper, printer);
                __helper.Pop();
            }
            if (nullablePrimitive != other.nullablePrimitive) CodeGenImplTools.LogCompError(__helper, "nullablePrimitive", printer, other.nullablePrimitive, nullablePrimitive);
            if (CodeGenImplTools.CompareNull(__helper, "nullableReactiveValue", printer, nullableReactiveValue.value, other.nullableReactiveValue.value)) {
                __helper.Push("nullableReactiveValue");
                nullableReactiveValue.value.CompareCheck(other.nullableReactiveValue.value, __helper, printer);
                __helper.Pop();
            }
            if (CodeGenImplTools.CompareNullable(__helper, "nullableStruct", printer, nullableStruct, other.nullableStruct)) {
                __helper.Push("nullableStruct");
                nullableStruct.Value.CompareCheck(other.nullableStruct.Value, __helper, printer);
                __helper.Pop();
            }
            if (CodeGenImplTools.CompareNullable(__helper, "nullableStructWithGenerationTags", printer, nullableStructWithGenerationTags, other.nullableStructWithGenerationTags)) {
                __helper.Push("nullableStructWithGenerationTags");
                nullableStructWithGenerationTags.Value.CompareCheck(other.nullableStructWithGenerationTags.Value, __helper, printer);
                __helper.Pop();
            }
            if (CodeGenImplTools.CompareNull(__helper, "otherData", printer, otherData, other.otherData)) {
                __helper.Push("otherData");
                otherData.CompareCheck(other.otherData, __helper, printer);
                __helper.Pop();
            }
            __helper.Push("otherData2");
            otherData2.CompareCheck(other.otherData2, __helper, printer);
            __helper.Pop();
            __helper.Push("reactiveCollections");
            reactiveCollections.CompareCheck(other.reactiveCollections, __helper, printer);
            __helper.Pop();
            if (reactiveNullablePrimitive.value != other.reactiveNullablePrimitive.value) CodeGenImplTools.LogCompError(__helper, "reactiveNullablePrimitive", printer, other.reactiveNullablePrimitive.value, reactiveNullablePrimitive.value);
            if (CodeGenImplTools.CompareNullable(__helper, "reactiveNullableStruct", printer, reactiveNullableStruct.value, other.reactiveNullableStruct.value)) {
                __helper.Push("reactiveNullableStruct");
                reactiveNullableStruct.value.Value.CompareCheck(other.reactiveNullableStruct.value.Value, __helper, printer);
                __helper.Pop();
            }
            if (CodeGenImplTools.CompareNullable(__helper, "reactiveNullableStructWithGenerationTags", printer, reactiveNullableStructWithGenerationTags.value, other.reactiveNullableStructWithGenerationTags.value)) {
                __helper.Push("reactiveNullableStructWithGenerationTags");
                reactiveNullableStructWithGenerationTags.value.Value.CompareCheck(other.reactiveNullableStructWithGenerationTags.value.Value, __helper, printer);
                __helper.Pop();
            }
            __helper.Push("reactiveValue");
            reactiveValue.value.CompareCheck(other.reactiveValue.value, __helper, printer);
            __helper.Pop();
            if (stringFieldMustNotBeNull != other.stringFieldMustNotBeNull) CodeGenImplTools.LogCompError(__helper, "stringFieldMustNotBeNull", printer, other.stringFieldMustNotBeNull, stringFieldMustNotBeNull);
            if (stringFieldThatCanBeNull != other.stringFieldThatCanBeNull) CodeGenImplTools.LogCompError(__helper, "stringFieldThatCanBeNull", printer, other.stringFieldThatCanBeNull, stringFieldThatCanBeNull);
            if (stringProp != other.stringProp) CodeGenImplTools.LogCompError(__helper, "stringProp", printer, other.stringProp, stringProp);
            __helper.Push("vector");
            vector.CompareCheck(other.vector, __helper, printer);
            __helper.Pop();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "ancestorArray":
                ancestorArray.ReadFromJson(reader);
                break;
                case "arraysAreOk":
                arraysAreOk = arraysAreOk.ReadFromJson(reader);
                break;
                case "complexStructuresAreAlsoOk":
                complexStructuresAreAlsoOk.ReadFromJson(reader);
                break;
                case "dictsAreOk":
                dictsAreOk.ReadFromJson(reader);
                break;
                case "dictWithNullableValues":
                dictWithNullableValues.ReadFromJson(reader);
                break;
                case "enumValue":
                enumValue = System.Enum.Parse<ZergRush.Samples.SampleEnum>((string)reader.Value);
                break;
                case "externalClass":
                externalClass.ReadFromJson(reader);
                break;
                case "genericAncestorArray":
                genericAncestorArray.ReadFromJson(reader);
                break;
                case "genericWithCustomStruct":
                genericWithCustomStruct.ReadFromJson(reader);
                break;
                case "genericWithPrimitive":
                genericWithPrimitive.ReadFromJson(reader);
                break;
                case "intField":
                intField = (int)(Int64)reader.Value;
                break;
                case "listOfNullablePrimitives":
                listOfNullablePrimitives.ReadFromJson(reader);
                break;
                case "listsOfDataAreOk":
                listsOfDataAreOk.ReadFromJson(reader);
                break;
                case "listsOfPrimitivesAreOk":
                listsOfPrimitivesAreOk.ReadFromJson(reader);
                break;
                case "nestedReactiveNullablePrimitive":
                if (reader.TokenType == JsonToken.Null) {
                    nestedReactiveNullablePrimitive.value.value.value = null;
                }
                else {
                    var __nestedReactiveNullablePrimitive_value_value_value = nestedReactiveNullablePrimitive.value.value.value.GetValueOrDefault();
                    __nestedReactiveNullablePrimitive_value_value_value = (int)(Int64)reader.Value;
                    nestedReactiveNullablePrimitive.value.value.value = __nestedReactiveNullablePrimitive_value_value_value;
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
                case "nullablePrimitive":
                if (reader.TokenType == JsonToken.Null) {
                    nullablePrimitive = null;
                }
                else {
                    var __nullablePrimitive = nullablePrimitive.GetValueOrDefault();
                    __nullablePrimitive = (int)(Int64)reader.Value;
                    nullablePrimitive = __nullablePrimitive;
                }
                break;
                case "nullableReactiveValue":
                if (reader.TokenType == JsonToken.Null) {
                    nullableReactiveValue.value = null;
                }
                else { 
                    if (nullableReactiveValue.value == null) {
                        nullableReactiveValue.value = new ZergRush.Samples.OtherData();
                    }
                    nullableReactiveValue.value.ReadFromJson(reader);
                }
                break;
                case "nullableStruct":
                if (reader.TokenType == JsonToken.Null) {
                    nullableStruct = null;
                }
                else {
                    var __nullableStruct = nullableStruct.GetValueOrDefault();
                    __nullableStruct = (UnityEngine.Vector3)reader.ReadFromJsonUnityEngine_Vector3();
                    nullableStruct = __nullableStruct;
                }
                break;
                case "nullableStructWithGenerationTags":
                if (reader.TokenType == JsonToken.Null) {
                    nullableStructWithGenerationTags = null;
                }
                else {
                    var __nullableStructWithGenerationTags = nullableStructWithGenerationTags.GetValueOrDefault();
                    __nullableStructWithGenerationTags.ReadFromJson(reader);
                    nullableStructWithGenerationTags = __nullableStructWithGenerationTags;
                }
                break;
                case "otherData":
                if (reader.TokenType == JsonToken.Null) {
                    otherData = null;
                }
                else { 
                    if (otherData == null) {
                        otherData = new ZergRush.Samples.OtherData();
                    }
                    otherData.ReadFromJson(reader);
                }
                break;
                case "otherData2":
                otherData2.ReadFromJson(reader);
                break;
                case "reactiveCollections":
                reactiveCollections.ReadFromJson(reader);
                break;
                case "reactiveNullablePrimitive":
                if (reader.TokenType == JsonToken.Null) {
                    reactiveNullablePrimitive.value = null;
                }
                else {
                    var __reactiveNullablePrimitive_value = reactiveNullablePrimitive.value.GetValueOrDefault();
                    __reactiveNullablePrimitive_value = (int)(Int64)reader.Value;
                    reactiveNullablePrimitive.value = __reactiveNullablePrimitive_value;
                }
                break;
                case "reactiveNullableStruct":
                if (reader.TokenType == JsonToken.Null) {
                    reactiveNullableStruct.value = null;
                }
                else {
                    var __reactiveNullableStruct_value = reactiveNullableStruct.value.GetValueOrDefault();
                    __reactiveNullableStruct_value = (UnityEngine.Vector3)reader.ReadFromJsonUnityEngine_Vector3();
                    reactiveNullableStruct.value = __reactiveNullableStruct_value;
                }
                break;
                case "reactiveNullableStructWithGenerationTags":
                if (reader.TokenType == JsonToken.Null) {
                    reactiveNullableStructWithGenerationTags.value = null;
                }
                else {
                    var __reactiveNullableStructWithGenerationTags_value = reactiveNullableStructWithGenerationTags.value.GetValueOrDefault();
                    __reactiveNullableStructWithGenerationTags_value.ReadFromJson(reader);
                    reactiveNullableStructWithGenerationTags.value = __reactiveNullableStructWithGenerationTags_value;
                }
                break;
                case "reactiveValue":
                reactiveValue.value.ReadFromJson(reader);
                break;
                case "stringFieldMustNotBeNull":
                stringFieldMustNotBeNull = (string) reader.Value;
                break;
                case "stringFieldThatCanBeNull":
                if (reader.TokenType == JsonToken.Null) {
                    stringFieldThatCanBeNull = null;
                }
                else { 
                    stringFieldThatCanBeNull = (string) reader.Value;
                }
                break;
                case "stringProp":
                stringProp = (string) reader.Value;
                break;
                case "vector":
                vector = (UnityEngine.Vector3)reader.ReadFromJsonUnityEngine_Vector3();
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("ancestorArray");
            ancestorArray.WriteJson(writer);
            writer.WritePropertyName("arraysAreOk");
            arraysAreOk.WriteJson(writer);
            writer.WritePropertyName("complexStructuresAreAlsoOk");
            complexStructuresAreAlsoOk.WriteJson(writer);
            writer.WritePropertyName("dictsAreOk");
            dictsAreOk.WriteJson(writer);
            writer.WritePropertyName("dictWithNullableValues");
            dictWithNullableValues.WriteJson(writer);
            writer.WritePropertyName("enumValue");
            writer.WriteValue(enumValue.ToString());
            writer.WritePropertyName("externalClass");
            externalClass.WriteJson(writer);
            writer.WritePropertyName("genericAncestorArray");
            genericAncestorArray.WriteJson(writer);
            writer.WritePropertyName("genericWithCustomStruct");
            genericWithCustomStruct.WriteJson(writer);
            writer.WritePropertyName("genericWithPrimitive");
            genericWithPrimitive.WriteJson(writer);
            writer.WritePropertyName("intField");
            writer.WriteValue(intField);
            writer.WritePropertyName("listOfNullablePrimitives");
            listOfNullablePrimitives.WriteJson(writer);
            writer.WritePropertyName("listsOfDataAreOk");
            listsOfDataAreOk.WriteJson(writer);
            writer.WritePropertyName("listsOfPrimitivesAreOk");
            listsOfPrimitivesAreOk.WriteJson(writer);
            if (!(nestedReactiveNullablePrimitive.value.value.value.HasValue))
            {
                writer.WritePropertyName("nestedReactiveNullablePrimitive");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nestedReactiveNullablePrimitive");
                writer.WriteValue(nestedReactiveNullablePrimitive.value.value.value.Value);
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
            if (!(nullablePrimitive.HasValue))
            {
                writer.WritePropertyName("nullablePrimitive");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullablePrimitive");
                writer.WriteValue(nullablePrimitive.Value);
            }
            if (!(nullableReactiveValue.value != null))
            {
                writer.WritePropertyName("nullableReactiveValue");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableReactiveValue");
                nullableReactiveValue.value.WriteJson(writer);
            }
            if (!(nullableStruct.HasValue))
            {
                writer.WritePropertyName("nullableStruct");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableStruct");
                nullableStruct.Value.WriteJson(writer);
            }
            if (!(nullableStructWithGenerationTags.HasValue))
            {
                writer.WritePropertyName("nullableStructWithGenerationTags");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("nullableStructWithGenerationTags");
                nullableStructWithGenerationTags.Value.WriteJson(writer);
            }
            if (!(otherData != null))
            {
                writer.WritePropertyName("otherData");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("otherData");
                otherData.WriteJson(writer);
            }
            writer.WritePropertyName("otherData2");
            otherData2.WriteJson(writer);
            writer.WritePropertyName("reactiveCollections");
            reactiveCollections.WriteJson(writer);
            if (!(reactiveNullablePrimitive.value.HasValue))
            {
                writer.WritePropertyName("reactiveNullablePrimitive");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("reactiveNullablePrimitive");
                writer.WriteValue(reactiveNullablePrimitive.value.Value);
            }
            if (!(reactiveNullableStruct.value.HasValue))
            {
                writer.WritePropertyName("reactiveNullableStruct");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("reactiveNullableStruct");
                reactiveNullableStruct.value.Value.WriteJson(writer);
            }
            if (!(reactiveNullableStructWithGenerationTags.value.HasValue))
            {
                writer.WritePropertyName("reactiveNullableStructWithGenerationTags");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("reactiveNullableStructWithGenerationTags");
                reactiveNullableStructWithGenerationTags.value.Value.WriteJson(writer);
            }
            writer.WritePropertyName("reactiveValue");
            reactiveValue.value.WriteJson(writer);
            writer.WritePropertyName("stringFieldMustNotBeNull");
            writer.WriteValue(stringFieldMustNotBeNull);
            if (!(stringFieldThatCanBeNull != null))
            {
                writer.WritePropertyName("stringFieldThatCanBeNull");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("stringFieldThatCanBeNull");
                writer.WriteValue(stringFieldThatCanBeNull);
            }
            writer.WritePropertyName("stringProp");
            writer.WriteValue(stringProp);
            writer.WritePropertyName("vector");
            vector.WriteJson(writer);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.CodeGenSamples;
        }
        public virtual object NewInst() 
        {
        return new CodeGenSamples();
        }
        public static ZergRush.Samples.CodeGenSamples CreatePolymorphic(CodeGenSamplesType __classId) 
        {
        return (ZergRush.Samples.CodeGenSamples)ZergRush.Samples.CodeGenSamples.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif

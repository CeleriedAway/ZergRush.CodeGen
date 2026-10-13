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

    public partial class JsonReferenceRoot : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonReferenceRoot>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonReferenceRoot>, IJsonSerializable, IsMultiRef
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.JsonReferenceRoot other, ZRUpdateFromHelper __helper) 
        {
            var __first = first;
            if (other.first == null) {
                __first = null;
            }
            else { 
                var __firstClassId = other.first.GetClassId();
                if (__first == null || __first.GetClassId() != __firstClassId) {
                    __first = (ZergRush.CodeGen.Tests.JsonPolyReference)other.first.NewInst();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.first, ref __first)) __first.UpdateFrom(other.first, __helper);
            }
            first = __first;
            many.UpdateFrom(other.many, __helper);
            var __plainFirst = plainFirst;
            if (other.plainFirst == null) {
                __plainFirst = null;
            }
            else { 
                if (__plainFirst == null) {
                    __plainFirst = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.plainFirst, ref __plainFirst)) __plainFirst.UpdateFrom(other.plainFirst, __helper);
            }
            plainFirst = __plainFirst;
            var __plainSecond = plainSecond;
            if (other.plainSecond == null) {
                __plainSecond = null;
            }
            else { 
                if (__plainSecond == null) {
                    __plainSecond = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.plainSecond, ref __plainSecond)) __plainSecond.UpdateFrom(other.plainSecond, __helper);
            }
            plainSecond = __plainSecond;
            var __second = second;
            if (other.second == null) {
                __second = null;
            }
            else { 
                var __secondClassId = other.second.GetClassId();
                if (__second == null || __second.GetClassId() != __secondClassId) {
                    __second = (ZergRush.CodeGen.Tests.JsonPolyReference)other.second.NewInst();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.second, ref __second)) __second.UpdateFrom(other.second, __helper);
            }
            second = __second;
            var __self = self;
            if (other.self == null) {
                __self = null;
            }
            else { 
                if (__self == null) {
                    __self = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                }
                if (!__helper.TryLoadAlreadyUpdated(other.self, ref __self)) __self.UpdateFrom(other.self, __helper);
            }
            self = __self;
            tail = other.tail;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            var __first = first;
            if (!reader.ReadBoolean()) {
                __first = null;
            }
            else { 
                var __firstClassId = reader.ReadUInt16();
                if (__first == null || __first.GetClassId() != __firstClassId) {
                    __first = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__firstClassId);
                }
                reader.ReadFromRef(ref __first);
            }
            first = __first;
            many.Deserialize(reader);
            var __plainFirst = plainFirst;
            if (!reader.ReadBoolean()) {
                __plainFirst = null;
            }
            else { 
                if (__plainFirst == null) {
                    __plainFirst = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                reader.ReadFromRef(ref __plainFirst);
            }
            plainFirst = __plainFirst;
            var __plainSecond = plainSecond;
            if (!reader.ReadBoolean()) {
                __plainSecond = null;
            }
            else { 
                if (__plainSecond == null) {
                    __plainSecond = new ZergRush.CodeGen.Tests.JsonPlainReference();
                }
                reader.ReadFromRef(ref __plainSecond);
            }
            plainSecond = __plainSecond;
            var __second = second;
            if (!reader.ReadBoolean()) {
                __second = null;
            }
            else { 
                var __secondClassId = reader.ReadUInt16();
                if (__second == null || __second.GetClassId() != __secondClassId) {
                    __second = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__secondClassId);
                }
                reader.ReadFromRef(ref __second);
            }
            second = __second;
            var __self = self;
            if (!reader.ReadBoolean()) {
                __self = null;
            }
            else { 
                if (__self == null) {
                    __self = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                }
                reader.ReadFromRef(ref __self);
            }
            self = __self;
            tail = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            if (!(first != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(first.GetClassId());
                writer.WriteObjectWithRef(first);
            }
            many.Serialize(writer);
            if (!(plainFirst != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.WriteObjectWithRef(plainFirst);
            }
            if (!(plainSecond != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.WriteObjectWithRef(plainSecond);
            }
            if (!(second != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(second.GetClassId());
                writer.WriteObjectWithRef(second);
            }
            if (!(self != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.WriteObjectWithRef(self);
            }
            writer.Write(tail);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1084576410;
            hash += hash << 11; hash ^= hash >> 7;
            hash += first != null ? __helper.CalculateHash(first) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += many.CalculateHash(__helper);
            hash += hash << 11; hash ^= hash >> 7;
            hash += plainFirst != null ? __helper.CalculateHash(plainFirst) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += plainSecond != null ? __helper.CalculateHash(plainSecond) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += second != null ? __helper.CalculateHash(second) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += self != null ? __helper.CalculateHash(self) : 345093625;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)tail;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  JsonReferenceRoot() 
        {
            many = new System.Collections.Generic.List<ZergRush.CodeGen.Tests.JsonPolyReference>();
        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.JsonReferenceRoot other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (CodeGenImplTools.CompareNull(__helper, "first", printer, first, other.first)) {
                if (CodeGenImplTools.CompareClassId(__helper, "first", printer, first, other.first)) {
                    if (__helper.NeedCompareCheck("first", printer, first, other.first)) {
                        __helper.Push("first");
                        first.CompareCheck(other.first, __helper, printer);
                        __helper.Pop();
                    }
                }
            }
            __helper.Push("many");
            many.CompareCheck(other.many, __helper, printer);
            __helper.Pop();
            if (CodeGenImplTools.CompareNull(__helper, "plainFirst", printer, plainFirst, other.plainFirst)) {
                if (__helper.NeedCompareCheck("plainFirst", printer, plainFirst, other.plainFirst)) {
                    __helper.Push("plainFirst");
                    plainFirst.CompareCheck(other.plainFirst, __helper, printer);
                    __helper.Pop();
                }
            }
            if (CodeGenImplTools.CompareNull(__helper, "plainSecond", printer, plainSecond, other.plainSecond)) {
                if (__helper.NeedCompareCheck("plainSecond", printer, plainSecond, other.plainSecond)) {
                    __helper.Push("plainSecond");
                    plainSecond.CompareCheck(other.plainSecond, __helper, printer);
                    __helper.Pop();
                }
            }
            if (CodeGenImplTools.CompareNull(__helper, "second", printer, second, other.second)) {
                if (CodeGenImplTools.CompareClassId(__helper, "second", printer, second, other.second)) {
                    if (__helper.NeedCompareCheck("second", printer, second, other.second)) {
                        __helper.Push("second");
                        second.CompareCheck(other.second, __helper, printer);
                        __helper.Pop();
                    }
                }
            }
            if (CodeGenImplTools.CompareNull(__helper, "self", printer, self, other.self)) {
                if (__helper.NeedCompareCheck("self", printer, self, other.self)) {
                    __helper.Push("self");
                    self.CompareCheck(other.self, __helper, printer);
                    __helper.Pop();
                }
            }
            if (tail != other.tail) CodeGenImplTools.LogCompError(__helper, "tail", printer, other.tail, tail);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "first":
                if (reader.TokenType == JsonToken.Null) {
                    first = null;
                }
                else { 
                    var __firstFrame = reader.BeginObject(true, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPolyReference>(__firstFrame, out var __firstFrameValue)) first = __firstFrameValue;
                    else
                    {
                        first = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__firstFrame.ClassId);
                    }
                    reader.ReadObject(first, __firstFrame);
                }
                break;
                case "many":
                many.ReadFromJson(reader);
                break;
                case "plainFirst":
                if (reader.TokenType == JsonToken.Null) {
                    plainFirst = null;
                }
                else { 
                    var __plainFirstFrame = reader.BeginObject(false, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPlainReference>(__plainFirstFrame, out var __plainFirstFrameValue)) plainFirst = __plainFirstFrameValue;
                    else
                    {
                        plainFirst = new ZergRush.CodeGen.Tests.JsonPlainReference();
                    }
                    reader.ReadObject(plainFirst, __plainFirstFrame);
                }
                break;
                case "plainSecond":
                if (reader.TokenType == JsonToken.Null) {
                    plainSecond = null;
                }
                else { 
                    var __plainSecondFrame = reader.BeginObject(false, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPlainReference>(__plainSecondFrame, out var __plainSecondFrameValue)) plainSecond = __plainSecondFrameValue;
                    else
                    {
                        plainSecond = new ZergRush.CodeGen.Tests.JsonPlainReference();
                    }
                    reader.ReadObject(plainSecond, __plainSecondFrame);
                }
                break;
                case "second":
                if (reader.TokenType == JsonToken.Null) {
                    second = null;
                }
                else { 
                    var __secondFrame = reader.BeginObject(true, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonPolyReference>(__secondFrame, out var __secondFrameValue)) second = __secondFrameValue;
                    else
                    {
                        second = (ZergRush.CodeGen.Tests.JsonPolyReference)ZergRush.CodeGen.Tests.JsonPolyReference.CreatePolymorphic(__secondFrame.ClassId);
                    }
                    reader.ReadObject(second, __secondFrame);
                }
                break;
                case "self":
                if (reader.TokenType == JsonToken.Null) {
                    self = null;
                }
                else { 
                    var __selfFrame = reader.BeginObject(false, true);
                    if (reader.TryGetReference<ZergRush.CodeGen.Tests.JsonReferenceRoot>(__selfFrame, out var __selfFrameValue)) self = __selfFrameValue;
                    else
                    {
                        self = new ZergRush.CodeGen.Tests.JsonReferenceRoot();
                    }
                    reader.ReadObject(self, __selfFrame);
                }
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
            if (!(first != null))
            {
                writer.WritePropertyName("first");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("first");
                writer.WriteObjectWithRef(first);
            }
            writer.WritePropertyName("many");
            many.WriteJson(writer);
            if (!(plainFirst != null))
            {
                writer.WritePropertyName("plainFirst");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("plainFirst");
                writer.WriteObjectWithRef(plainFirst);
            }
            if (!(plainSecond != null))
            {
                writer.WritePropertyName("plainSecond");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("plainSecond");
                writer.WriteObjectWithRef(plainSecond);
            }
            if (!(second != null))
            {
                writer.WritePropertyName("second");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("second");
                writer.WriteObjectWithRef(second);
            }
            if (!(self != null))
            {
                writer.WritePropertyName("self");
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName("self");
                writer.WriteObjectWithRef(self);
            }
            writer.WritePropertyName("tail");
            writer.WriteValue(tail);
        }
    }
}
#endif

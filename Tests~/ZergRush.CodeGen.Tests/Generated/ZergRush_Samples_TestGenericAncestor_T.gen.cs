using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.Samples {

    public partial class TestGenericAncestor<T> : IUpdatableFrom<ZergRush.Samples.TestGenericAncestor<T>>, IUpdatableFrom<ZergRush.Samples.TestPolyGenericParent>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Samples.TestPolyGenericParent>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public override void UpdateFrom(ZergRush.Samples.TestPolyGenericParent other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.Samples.TestGenericAncestor<T>)other;
            normalField = otherConcrete.normalField;
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                var __genericOther0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)otherConcrete;
                var __genericThis0_genericFieldClassId = __genericOther0.genericField.GetClassId();
                if (__genericThis0.genericField == null || __genericThis0.genericField.GetClassId() != __genericThis0_genericFieldClassId) {
                    __genericThis0.genericField = (ZergRush.Samples.CodeGenSamples)__genericOther0.genericField.NewInst();
                }
                __genericThis0.genericField.UpdateFrom(__genericOther0.genericField, __helper);
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                var __genericOther1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)otherConcrete;
                __genericThis1.genericField = __genericOther1.genericField;
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public void UpdateFrom(ZergRush.Samples.TestGenericAncestor<T> other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.Samples.TestPolyGenericParent)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            normalField = reader.ReadString();
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                var __genericThis0_genericFieldClassId = reader.ReadUInt16();
                if (__genericThis0.genericField == null || __genericThis0.genericField.GetClassId() != __genericThis0_genericFieldClassId) {
                    __genericThis0.genericField = (ZergRush.Samples.CodeGenSamples)ZergRush.Samples.CodeGenSamples.CreatePolymorphic(__genericThis0_genericFieldClassId);
                }
                __genericThis0.genericField.Deserialize(reader);
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                __genericThis1.genericField = reader.ReadInt32();
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(normalField);
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                writer.Write(__genericThis0.genericField.GetClassId());
                __genericThis0.genericField.Serialize(writer);
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                writer.Write(__genericThis1.genericField);
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)1435885116;
            hash += hash << 11; hash ^= hash >> 7;
            hash += CodeGenImplTools.CalculateStringHash(normalField);
            hash += hash << 11; hash ^= hash >> 7;
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                hash += __genericThis0.genericField.CalculateHash(__helper);
                hash += hash << 11; hash ^= hash >> 7;
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                hash += (ulong)__genericThis1.genericField;
                hash += hash << 11; hash ^= hash >> 7;
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
            return hash;
        }
        public  TestGenericAncestor() 
        {
            normalField = string.Empty;
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                __genericThis0.genericField = (ZergRush.Samples.CodeGenSamples)new ZergRush.Samples.CodeGenSamples();
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public override void CompareCheck(ZergRush.Samples.TestPolyGenericParent other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.Samples.TestGenericAncestor<T>)other;
            if (normalField != otherConcrete.normalField) CodeGenImplTools.LogCompError(__helper, "normalField", printer, otherConcrete.normalField, normalField);
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                var __genericOther0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)otherConcrete;
                if (CodeGenImplTools.CompareClassId(__helper, "genericField", printer, __genericThis0.genericField, __genericOther0.genericField)) {
                    __helper.Push("genericField");
                    __genericThis0.genericField.CompareCheck(__genericOther0.genericField, __helper, printer);
                    __helper.Pop();
                }
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                var __genericOther1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)otherConcrete;
                if (__genericThis1.genericField != __genericOther1.genericField) CodeGenImplTools.LogCompError(__helper, "genericField", printer, __genericOther1.genericField, __genericThis1.genericField);
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "normalField":
                normalField = (string) reader.Value;
                return true;
                default: break;
            }
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                switch(__name)
                {
                    case "genericField":
                    var __genericThis0_genericFieldClassId = reader.ReadJsonClassId();
                    if (__genericThis0.genericField == null || __genericThis0.genericField.GetClassId() != __genericThis0_genericFieldClassId) {
                        __genericThis0.genericField = (ZergRush.Samples.CodeGenSamples)ZergRush.Samples.CodeGenSamples.CreatePolymorphic(__genericThis0_genericFieldClassId);
                    }
                    __genericThis0.genericField.ReadFromJson(reader);
                    return true;
                    default: return false;
                }
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                switch(__name)
                {
                    case "genericField":
                    __genericThis1.genericField = (int)(Int64)reader.Value;
                    return true;
                    default: return false;
                }
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("normalField");
            writer.WriteValue(normalField);
            if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples))
            {
                var __genericThis0 = (ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>)(object)this;
                writer.WritePropertyName("genericField");
                __genericThis0.genericField.WriteJson(writer);
            }
            else if (typeof(T) == typeof(int))
            {
                var __genericThis1 = (ZergRush.Samples.TestGenericAncestor<int>)(object)this;
                writer.WritePropertyName("genericField");
                writer.WriteValue(__genericThis1.genericField);
            }
            else
            {
                throw new System.NotSupportedException($"Generic specialization '{GetType()}' is not registered for ZergRush.Samples.TestGenericAncestor<T>.");
            }
        }
        public override ushort GetClassId() 
        {
         if (typeof(T) == typeof(ZergRush.Samples.CodeGenSamples)) {
            return (ushort)Types.TestGenericAncestor_CodeGenSamples;
        }
        else  if (typeof(T) == typeof(int)) {
            return (ushort)Types.TestGenericAncestor_Int32;
        }
        return 0;
        }
        public override object NewInst() 
        {
        return new TestGenericAncestor<T>();
        }
    }
}
#endif

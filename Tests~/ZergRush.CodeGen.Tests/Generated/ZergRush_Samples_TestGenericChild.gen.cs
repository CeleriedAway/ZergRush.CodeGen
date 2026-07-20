using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.Samples {

    public partial class TestGenericChild : IUpdatableFrom<ZergRush.Samples.TestGenericChild>, IUpdatableFrom<ZergRush.Samples.TestPolyGenericParent>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Samples.TestPolyGenericParent>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public override void UpdateFrom(ZergRush.Samples.TestPolyGenericParent other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.Samples.TestGenericChild)other;
            additionalField = otherConcrete.additionalField;
        }
        public void UpdateFrom(ZergRush.Samples.TestGenericChild other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.Samples.TestPolyGenericParent)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            additionalField = reader.ReadInt32();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(additionalField);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)751998280;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)additionalField;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  TestGenericChild() 
        {

        }
        public override void CompareCheck(ZergRush.Samples.TestPolyGenericParent other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.Samples.TestGenericChild)other;
            if (additionalField != otherConcrete.additionalField) CodeGenImplTools.LogCompError(__helper, "additionalField", printer, otherConcrete.additionalField, additionalField);
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "additionalField":
                additionalField = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("additionalField");
            writer.WriteValue(additionalField);
        }
        public override ushort GetClassId() 
        {
        return (ushort)Types.TestGenericChild;
        }
        public override object NewInst() 
        {
        return new TestGenericChild();
        }
    }
}
#endif

using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.Samples {

    public partial class TestPolyGenericParent : IUpdatableFrom<ZergRush.Samples.TestPolyGenericParent>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Samples.TestPolyGenericParent>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            TestPolyGenericParent = 1,
            TestGenericAncestor_Int32 = 2,
            TestGenericAncestor_CodeGenSamples = 3,
            TestGenericChild = 4,
        }
        static Func<TestPolyGenericParent> [] polymorphConstructors = new Func<TestPolyGenericParent> [] {
            () => null, // 0
            () => new ZergRush.Samples.TestPolyGenericParent(), // 1
            () => new ZergRush.Samples.TestGenericAncestor<int>(), // 2
            () => new ZergRush.Samples.TestGenericAncestor<ZergRush.Samples.CodeGenSamples>(), // 3
            () => new ZergRush.Samples.TestGenericChild(), // 4
        };
        public static TestPolyGenericParent CreatePolymorphic(ushort typeId) {
            return polymorphConstructors[typeId]();
        }
        public TestPolyGenericParentType type => (TestPolyGenericParentType) GetClassId();
        public virtual void UpdateFrom(ZergRush.Samples.TestPolyGenericParent other, ZRUpdateFromHelper __helper) 
        {
            intField = other.intField;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            intField = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(intField);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1090383522;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)intField;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public virtual void CompareCheck(ZergRush.Samples.TestPolyGenericParent other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (intField != other.intField) CodeGenImplTools.LogCompError(__helper, "intField", printer, other.intField, intField);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "intField":
                intField = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("intField");
            writer.WriteValue(intField);
        }
        public  TestPolyGenericParent() 
        {

        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.TestPolyGenericParent;
        }
        public virtual object NewInst() 
        {
        return new TestPolyGenericParent();
        }
        public static ZergRush.Samples.TestPolyGenericParent CreatePolymorphic(TestPolyGenericParentType __classId) 
        {
        return (ZergRush.Samples.TestPolyGenericParent)ZergRush.Samples.TestPolyGenericParent.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif

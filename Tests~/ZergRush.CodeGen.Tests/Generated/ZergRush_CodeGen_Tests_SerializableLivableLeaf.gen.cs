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

    public partial class SerializableLivableLeaf : IUpdatableFrom<ZergRush.CodeGen.Tests.SerializableLivableLeaf>, IUpdatableFrom<ZergRush.Alive.Livable>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Alive.Livable>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public enum Types : ushort
        {
            SerializableLivableLeaf = 1,
        }
        static Func<SerializableLivableLeaf> [] polymorphConstructors = new Func<SerializableLivableLeaf> [] {
            () => null, // 0
            () => new ZergRush.CodeGen.Tests.SerializableLivableLeaf(), // 1
        };
        public static SerializableLivableLeaf CreatePolymorphic(ushort typeId) {
            return polymorphConstructors[typeId]();
        }
        public SerializableLivableLeafType type => (SerializableLivableLeafType) GetClassId();
        public override void UpdateFrom(ZergRush.Alive.Livable other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.CodeGen.Tests.SerializableLivableLeaf)other;
            value = otherConcrete.value;
        }
        public void UpdateFrom(ZergRush.CodeGen.Tests.SerializableLivableLeaf other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.Alive.Livable)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            value = reader.ReadInt32();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(value);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)1336561387;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)value;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override void Enlive() 
        {
            EnliveSelf();
            EnliveChildren();
        }
        public override void Mortify() 
        {
            MortifySelf();
            MortifyChildren();
        }
        protected override void EnliveChildren() 
        {
            base.EnliveChildren();

        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();

        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);

        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();

        }
        public  SerializableLivableLeaf() 
        {

        }
        public override void CompareCheck(ZergRush.Alive.Livable other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.CodeGen.Tests.SerializableLivableLeaf)other;
            if (value != otherConcrete.value) CodeGenImplTools.LogCompError(__helper, "value", printer, otherConcrete.value, value);
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "value":
                value = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("value");
            writer.WriteValue(value);
        }
        public virtual ushort GetClassId() 
        {
        return (ushort)Types.SerializableLivableLeaf;
        }
        public virtual object NewInst() 
        {
        return new SerializableLivableLeaf();
        }
        public static ZergRush.CodeGen.Tests.SerializableLivableLeaf CreatePolymorphic(SerializableLivableLeafType __classId) 
        {
        return (ZergRush.CodeGen.Tests.SerializableLivableLeaf)ZergRush.CodeGen.Tests.SerializableLivableLeaf.CreatePolymorphic((ushort) __classId);
        }
    }
}
#endif

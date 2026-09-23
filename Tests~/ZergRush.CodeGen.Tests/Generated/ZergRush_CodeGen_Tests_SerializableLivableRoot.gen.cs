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

    public partial class SerializableLivableRoot : IUpdatableFrom<ZergRush.CodeGen.Tests.SerializableLivableRoot>, IUpdatableFrom<ZergRush.Alive.Livable>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.Alive.Livable>, IJsonSerializable
    {
        public override void UpdateFrom(ZergRush.Alive.Livable other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.CodeGen.Tests.SerializableLivableRoot)other;
            items.UpdateFrom(otherConcrete.items, __helper);
        }
        public void UpdateFrom(ZergRush.CodeGen.Tests.SerializableLivableRoot other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.Alive.Livable)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            items.Deserialize(reader);
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            items.Serialize(writer);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)6059112;
            hash += hash << 11; hash ^= hash >> 7;
            hash += items.CalculateHash(__helper);
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
            items.Enlive();
        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();
            items.Mortify();
        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);
            items.VisitNode(action);
        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            switch (localChildId)
            {
                case 1: return items;
            }
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();
            items.SetRootAndCarrier(root, this);
            items.__PropagateHierarchy();
        }
        public  SerializableLivableRoot() 
        {
            items = new ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.SerializableLivableLeaf>();
            items.livableAddressId = 1;
            root = this;
            __PropagateHierarchy();
        }
        public override void CompareCheck(ZergRush.Alive.Livable other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.CodeGen.Tests.SerializableLivableRoot)other;
            __helper.Push("items");
            items.CompareCheck(otherConcrete.items, __helper, printer);
            __helper.Pop();
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "items":
                items.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("items");
            items.WriteJson(writer);
        }
    }
}
#endif

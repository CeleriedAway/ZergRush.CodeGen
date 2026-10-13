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

    public partial class JsonPolyReferenceChild : IUpdatableFrom<ZergRush.CodeGen.Tests.JsonPolyReferenceChild>, IUpdatableFrom<ZergRush.CodeGen.Tests.JsonPolyReference>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.JsonPolyReference>, IJsonSerializable, IPolymorphable, ICloneInst
    {
        public override void UpdateFrom(ZergRush.CodeGen.Tests.JsonPolyReference other, ZRUpdateFromHelper __helper) 
        {
            base.UpdateFrom(other,__helper);
            var otherConcrete = (ZergRush.CodeGen.Tests.JsonPolyReferenceChild)other;
            extra = otherConcrete.extra;
        }
        public void UpdateFrom(ZergRush.CodeGen.Tests.JsonPolyReferenceChild other, ZRUpdateFromHelper __helper) 
        {
            this.UpdateFrom((ZergRush.CodeGen.Tests.JsonPolyReference)other, __helper);
        }
        public override void Deserialize(ZRBinaryReader reader) 
        {
            base.Deserialize(reader);
            extra = reader.ReadInt32();
        }
        public override void Serialize(ZRBinaryWriter writer) 
        {
            base.Serialize(writer);
            writer.Write(extra);
        }
        public override ulong CalculateHash(ZRHashHelper __helper) 
        {
            var baseVal = base.CalculateHash(__helper);
            ulong hash = baseVal;
            hash ^= (ulong)1444819801;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)extra;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public override void CompareCheck(ZergRush.CodeGen.Tests.JsonPolyReference other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            base.CompareCheck(other,__helper,printer);
            var otherConcrete = (ZergRush.CodeGen.Tests.JsonPolyReferenceChild)other;
            if (extra != otherConcrete.extra) CodeGenImplTools.LogCompError(__helper, "extra", printer, otherConcrete.extra, extra);
        }
        public override bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            if (base.ReadFromJsonField(reader, __name)) return true;
            switch(__name)
            {
                case "extra":
                extra = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public override void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            base.WriteJsonFields(writer);
            writer.WritePropertyName("extra");
            writer.WriteValue(extra);
        }
        public override ushort GetClassId() 
        {
        return (ushort)Types.JsonPolyReferenceChild;
        }
        public override object NewInst() 
        {
        return new JsonPolyReferenceChild();
        }
    }
}
#endif

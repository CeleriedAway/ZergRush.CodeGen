// ZergRush serialization schema: 2 (logical collections, replacement reads, strict JSON)
using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class ConfigReferenceHolder : IBinaryDeserializable, IBinarySerializable
    {
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            if (!reader.ReadBoolean()) {
                optionalReference = null;
            }
            else { 
                optionalReference = (ZergRush.CodeGen.Tests.ConfigReferenceItem)ZergRush.CodeGen.Tests.ConfigReferenceRoot.TryGetConfig(reader.ReadUInt64());
            }
            requiredReference = (ZergRush.CodeGen.Tests.ConfigReferenceItem)ZergRush.CodeGen.Tests.ConfigReferenceRoot.GetConfig(reader.ReadUInt64());
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            if (!(optionalReference != null)) writer.Write(false);
            else {
                writer.Write(true);
                writer.Write(optionalReference.UId());
            }
            writer.Write(requiredReference.UId());
        }
    }
}
#endif

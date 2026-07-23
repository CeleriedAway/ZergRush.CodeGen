using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
using System.IO;
using Newtonsoft.Json;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class ConfigStorageCoverageSamples : IUpdatableFrom<ZergRush.CodeGen.Tests.ConfigStorageCoverageSamples>, IBinaryDeserializable, IBinarySerializable, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.ConfigStorageCoverageSamples other, ZRUpdateFromHelper __helper) 
        {
            dictionary.UpdateFrom(other.dictionary, __helper);
            list.UpdateFrom(other.list, __helper);
            slot.UpdateFrom(other.slot, __helper);
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            dictionary.Deserialize(reader);
            list.Deserialize(reader);
            slot.Deserialize(reader);
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            dictionary.Serialize(writer);
            list.Serialize(writer);
            slot.Serialize(writer);
        }
        public  ConfigStorageCoverageSamples() 
        {
            dictionary = new ZergRush.Alive.ConfigStorageDict<string, ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>();
            list = new ZergRush.Alive.ConfigStorageList<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>();
            slot = new ZergRush.Alive.ConfigStorageSlot<ZergRush.CodeGen.Tests.ConfigStorageCoverageItem>();
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "dictionary":
                dictionary.ReadFromJson(reader);
                break;
                case "list":
                list.ReadFromJson(reader);
                break;
                case "slot":
                slot.ReadFromJson(reader);
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("dictionary");
            dictionary.WriteJson(writer);
            writer.WritePropertyName("list");
            list.WriteJson(writer);
            writer.WritePropertyName("slot");
            slot.WriteJson(writer);
        }
    }
}
#endif

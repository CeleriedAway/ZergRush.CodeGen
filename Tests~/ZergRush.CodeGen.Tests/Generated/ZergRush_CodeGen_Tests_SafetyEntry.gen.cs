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

    public partial class SafetyEntry : IUpdatableFrom<ZergRush.CodeGen.Tests.SafetyEntry>, IBinaryDeserializable, IBinarySerializable, IHashable, ICompareCheckable<ZergRush.CodeGen.Tests.SafetyEntry>, IJsonSerializable
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Tests.SafetyEntry other, ZRUpdateFromHelper __helper) 
        {
            playerId = other.playerId;
            score = other.score;
        }
        public virtual void Deserialize(ZRBinaryReader reader) 
        {
            playerId = reader.ReadInt64();
            score = reader.ReadInt32();
        }
        public virtual void Serialize(ZRBinaryWriter writer) 
        {
            writer.Write(playerId);
            writer.Write(score);
        }
        public virtual ulong CalculateHash(ZRHashHelper __helper) 
        {
            ulong hash = 345093625;
            hash ^= (ulong)1218368114;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)playerId;
            hash += hash << 11; hash ^= hash >> 7;
            hash += (ulong)score;
            hash += hash << 11; hash ^= hash >> 7;
            return hash;
        }
        public  SafetyEntry() 
        {

        }
        public virtual void CompareCheck(ZergRush.CodeGen.Tests.SafetyEntry other, ZRCompareCheckHelper __helper, Action<string> printer) 
        {
            if (playerId != other.playerId) CodeGenImplTools.LogCompError(__helper, "playerId", printer, other.playerId, playerId);
            if (score != other.score) CodeGenImplTools.LogCompError(__helper, "score", printer, other.score, score);
        }
        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string __name) 
        {
            switch(__name)
            {
                case "playerId":
                playerId = (long)(Int64)reader.Value;
                break;
                case "score":
                score = (int)(Int64)reader.Value;
                break;
                default: return false; break;
            }
            return true;
        }
        public virtual void WriteJsonFields(ZRJsonTextWriter writer) 
        {
            writer.WritePropertyName("playerId");
            writer.WriteValue(playerId);
            writer.WritePropertyName("score");
            writer.WriteValue(score);
        }
    }
}
#endif

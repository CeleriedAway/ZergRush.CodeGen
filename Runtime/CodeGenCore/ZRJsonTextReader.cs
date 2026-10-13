using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ZergRush
{
    public class ZRHashHelper
    {
        ZRObjectIDGenerator generator = new ();
        Dictionary<long, ulong> alreadyHashed = new();
        
        // call before use if want to reuse allocated memory
        public void Reuse()
        {
            generator.Clear();
            alreadyHashed.Clear();
        }
        
        public ulong CalculateHash<T>(T source) where T : IHashable
        {
            var id = generator.GetId(source, out var firstTime);
            if (firstTime)
            {
                alreadyHashed[id] = (ulong)id;
                var hash = source.CalculateHash(this);
                alreadyHashed[id] = hash;
                return hash ^ 0x2345096349;
            }
            else
            {
                return alreadyHashed[id];
            }
        }
    }
    
    public partial class ZRJsonTextReader : JsonTextReader
    {
        public DeserializationBudget Budget { get; set; } = new DeserializationBudget();

        public void ReadRequired()
        {
            do
            {
                if (!Read()) throw new JsonSerializationException("Unexpected end of JSON.");
            } while (TokenType == JsonToken.Comment);
        }

        public void RequireToken(JsonToken token)
        {
            if (TokenType != token) throw new JsonSerializationException("Expected " + token + ", got " + TokenType);
        }

        struct ReferenceEntry
        {
            internal IJsonSerializable Value;
            internal bool Defined;
        }

        readonly Dictionary<long, ReferenceEntry> currentObjects = new Dictionary<long, ReferenceEntry>();
        int undefinedReferences;

        public ZRJsonTextReader(TextReader reader) : base(reader)
        {
            DateParseHandling = DateParseHandling.None;
        }
        public ZRJsonTextReader(string str) : this(new StringReader(str))
        {
        }
        
        public void InitRootObject(IJsonSerializable obj)
        {
            currentObjects.Clear();
            undefinedReferences = 0;
            Budget.ReserveReference();
            currentObjects[1] = new ReferenceEntry { Value = obj, Defined = true };
        }

        public void ReadFromRef<T>(ref T t) where T : IJsonSerializable
        {
            if (TokenType == JsonToken.Null) { t = default; return; }
            var frame = BeginObject(t is IPolymorphable, true);
            if (TryGetReference<T>(frame, out var existing)) t = existing;
            ReadObject(t, frame);
        }
    }
}

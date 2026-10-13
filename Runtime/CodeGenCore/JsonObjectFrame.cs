using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ZergRush
{
    /// <summary>Value-type header. BeginObject receives StartObject/StartArray and leaves
    /// the payload's StartObject/Null. ReadObject/ResolveReference leave the matching
    /// outer EndObject/EndArray current, without consuming the next sibling.</summary>
    public struct JsonObjectFrame
    {
        public ushort ClassId { get; internal set; }
        public bool HasClassId { get; internal set; }
        public bool IsReference { get; internal set; }
        internal long ReferenceId;
        internal bool TracksReferences;
        internal bool Array;
        internal int Depth;
    }

    public partial class ZRJsonTextReader
    {
        public override bool Read()
        {
            bool result = base.Read();
            if (!result && Depth != 0) throw new JsonSerializationException("Unexpected end of JSON.");
            return result;
        }

        public ushort ReadClassIdValue()
        {
            if (TokenType != JsonToken.Integer || !(Value is long id) || id < 0 || id > ushort.MaxValue)
                throw new JsonSerializationException("Expected an integer class ID in [0, 65535].");
            return (ushort)id;
        }

        long ReadReferenceIdValue()
        {
            if (TokenType != JsonToken.String || !long.TryParse((string)Value, NumberStyles.None,
                    CultureInfo.InvariantCulture, out long id) || id <= 0)
                throw new JsonSerializationException("Expected a positive reference ID string.");
            return id;
        }

        public JsonObjectFrame BeginObject(bool polymorphic, bool referenceTracked = false)
        {
            var frame = new JsonObjectFrame
            {
                Depth = Depth, TracksReferences = referenceTracked,
                Array = polymorphic || referenceTracked, HasClassId = polymorphic
            };
            if (frame.Array)
            {
                RequireToken(JsonToken.StartArray);
                ReadRequired();
                if (polymorphic) frame.ClassId = ReadClassIdValue();
                else RequireToken(JsonToken.Null);
                ReadRequired();
                if (referenceTracked)
                {
                    frame.ReferenceId = ReadReferenceIdValue();
                    ReadRequired();
                    RequireToken(JsonToken.Boolean);
                    frame.IsReference = (bool)Value;
                    ReadRequired();
                }
            }
            RequireToken(frame.IsReference ? JsonToken.Null : JsonToken.StartObject);
            return frame;
        }

        static void ValidateType<T>(T value, JsonObjectFrame frame) where T : IJsonSerializable
        {
            if (value == null) throw new JsonSerializationException("Unknown class ID or null object instance.");
            if (frame.HasClassId && (!(value is IPolymorphable polymorphic) || polymorphic.GetClassId() != frame.ClassId))
                throw new JsonSerializationException("JSON class ID does not match the destination type. Use a polymorphic factory to replace the root.");
        }

        public T ResolveReference<T>(JsonObjectFrame frame) where T : IJsonSerializable
        {
            if (!frame.TracksReferences || !frame.IsReference ||
                !currentObjects.TryGetValue(frame.ReferenceId, out var existing) || !(existing.Value is T value))
                throw new JsonSerializationException("Unresolved or incompatible JSON reference.");
            ValidateType(value, frame);
            FinishObject(frame);
            return value;
        }

        /// <summary>Looks up an identity before generated code constructs anything. A forward
        /// reference creates the final instance once; its later definition fills that instance.</summary>
        public bool TryGetReference<T>(JsonObjectFrame frame, out T value) where T : IJsonSerializable
        {
            if (!frame.TracksReferences) throw new JsonSerializationException("Expected a reference envelope.");
            if (currentObjects.TryGetValue(frame.ReferenceId, out var existing))
            {
                if (!(existing.Value is T typed)) throw new JsonSerializationException("Incompatible JSON reference type.");
                ValidateType(typed, frame);
                value = typed;
                return true;
            }
            value = default;
            return false;
        }

        public void ReadObject<T>(T value, JsonObjectFrame frame) where T : IJsonSerializable
        {
            ValidateType(value, frame);
            if (frame.TracksReferences)
            {
                if (currentObjects.TryGetValue(frame.ReferenceId, out var existing))
                {
                    if (!ReferenceEquals(existing.Value, value)) throw new JsonSerializationException("Conflicting JSON reference instance.");
                    if (!frame.IsReference)
                    {
                        if (existing.Defined) throw new JsonSerializationException("Duplicate JSON reference definition.");
                        currentObjects[frame.ReferenceId] = new ReferenceEntry { Value = value, Defined = true };
                        undefinedReferences--;
                    }
                }
                else
                {
                    Budget.ReserveReference();
                    currentObjects.Add(frame.ReferenceId, new ReferenceEntry { Value = value, Defined = !frame.IsReference });
                    if (frame.IsReference) undefinedReferences++;
                }
            }
            if (frame.IsReference) { FinishObject(frame); return; }
            while (true)
            {
                ReadRequired();
                if (TokenType == JsonToken.EndObject) break;
                RequireToken(JsonToken.PropertyName);
                string name = (string)Value;
                if (frame.Array && (name == CodeGenImplTools.ClassIdName || name == "isRef" || name == "refId"))
                    throw new JsonSerializationException("Object metadata belongs in the array header; regenerate JSON readers and convert old saves.");
                ReadRequired();
                if (!value.ReadFromJsonField(this, name)) this.SkipObj();
            }
            FinishObject(frame);
        }

        void FinishObject(JsonObjectFrame frame)
        {
            if (frame.Array)
            {
                ReadRequired();
                RequireToken(JsonToken.EndArray);
            }
            else RequireToken(JsonToken.EndObject);
            if (Depth != frame.Depth) throw new JsonSerializationException("Invalid JSON object envelope boundary.");
        }

        public void CompleteRoot()
        {
            while (Read())
                if (TokenType != JsonToken.Comment) throw new JsonSerializationException("Trailing JSON content.");
            if (undefinedReferences != 0) throw new JsonSerializationException("JSON references have no definition.");
        }

        public T ReadPolymorphicRoot<T>(Func<ushort, T> factory) where T : class, IJsonSerializable
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (TokenType == JsonToken.None) ReadRequired();
            if (TokenType == JsonToken.Null) { CompleteRoot(); return null; }
            var frame = BeginObject(true);
            var value = factory(frame.ClassId);
            ValidateType(value, frame);
            InitRootObject(value);
            ReadObject(value, frame);
            CompleteRoot();
            return value;
        }
    }
}

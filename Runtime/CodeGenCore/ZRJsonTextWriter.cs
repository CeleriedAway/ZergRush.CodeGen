using System.IO;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZergRush
{
    public class ZRJsonTextWriter : JsonTextWriter
    {
        ObjectIDGenerator generator = new ObjectIDGenerator();

        public ZRJsonTextWriter(TextWriter textWriter) : base(textWriter)
        {
        }
        
        public void RegisterFirstObject(IJsonSerializable obj)
        {
            generator.GetId(obj, out bool firstTime);
            if (!firstTime)
            {
                throw new ZergRushException("Object was already registered");
            }
        }

        internal void RegisterRootIfNeeded(IJsonSerializable obj)
        {
            if (WriteState == WriteState.Start) generator.GetId(obj, out _);
        }

        public void WriteObjectWithRef(IJsonSerializable obj)
        {
            var writer = this;
            if (obj == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartArray();
            var polymorph = obj as IPolymorphable;
            if (polymorph != null)
            {
                writer.WriteValue(polymorph.GetClassId());
            }
            else writer.WriteNull();

            var refId = generator.GetId(obj, out bool firstTime);
            writer.WriteValue(refId.ToString(CultureInfo.InvariantCulture));
            writer.WriteValue(!firstTime);

            if (firstTime)
            {
                writer.WriteStartObject();
                obj.WriteJsonFields(writer);
                writer.WriteEndObject();
            }
            else writer.WriteNull();
            writer.WriteEndArray();
        }
    }
}

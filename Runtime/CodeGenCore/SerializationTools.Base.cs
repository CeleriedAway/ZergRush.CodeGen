using Newtonsoft.Json;
using ZergRush;

public interface IStableIdentifiable 
{
    public int stableId { get; }
}

public static partial class SerializationTools
{
    // Return if needs to check further

    public static uint CalculateHash(this byte[] array, ZRHashHelper _)
    {
        uint hash = 0;
        for (int i = 0; i < array.Length; i++)
        {
            hash += array[i];
            hash += hash << 10;
            hash ^= hash >> 6;
        }
        return hash;
    }

    public static void ReadSkipComments(this JsonTextReader reader)
    {
        while (reader.Read() && reader.TokenType == JsonToken.Comment)
        {
        }
    }

    public static void WriteJson(this IJsonSerializable obj, ZRJsonTextWriter writer)
    {
        if (obj == null)
        {
            writer.WriteNull();
            return;
        }

        writer.RegisterRootIfNeeded(obj);
        var polymorph = obj as IPolymorphable;
        if (polymorph != null)
        {
            writer.WriteStartArray();
            writer.WriteValue(polymorph.GetClassId());
        }
        writer.WriteStartObject();
        obj.WriteJsonFields(writer);
        writer.WriteEndObject();
        if (polymorph != null) writer.WriteEndArray();
    }

    public static T ReadAsJsonRoot<T>(this ZRJsonTextReader reader, T obj = null)
        where T : class, IJsonSerializable, new()
    {
        if (reader.TokenType == JsonToken.None) reader.ReadRequired();
        if (reader.TokenType == JsonToken.Null) { reader.CompleteRoot(); return null; }
        if (obj == null) obj = new T();
        obj.ReadRootFromJson(reader);
        return obj;
    }
}

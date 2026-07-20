namespace ZergRush.Alive
{
    using System;
    using CodeGen;
    
    /// <summary>
    /// Base class for all config members.
    /// Inheritors must defile one or more fields with [UIDComponent] to check equality of config members.
    /// Usually, it`s something unique like "string id" field.
    /// See example below.
    /// </summary>
    [GenZergRushFolder, Immutable, GenTaskCustomImpl(GenTaskFlags.UIDGen)]
    [GenTask((GenTaskFlags.ConfigData | GenTaskFlags.UIDGen) & ~GenTaskFlags.PolymorphicConstruction)]
    public partial class LoadableConfig : IUniquelyIdentifiable, IBinaryDeserializable, IBinarySerializable,
        IHashable, IJsonSerializable
    {
        public ulong id => UId();
        public virtual ulong UId() => 0;

        public virtual void Deserialize(ZRBinaryReader reader)
        {
        }

        public virtual void Serialize(ZRBinaryWriter writer)
        {
        }

        public virtual ulong CalculateHash(ZRHashHelper helper)
        {
            ulong hash = 345093625;
            hash ^= 1057538465;
            hash += hash << 11;
            hash ^= hash >> 7;
            return hash;
        }

        public virtual void CollectConfigs(ConfigRegister collection)
        {
        }

        public virtual bool ReadFromJsonField(ZRJsonTextReader reader, string name)
        {
            return false;
        }

        public virtual void WriteJsonFields(ZRJsonTextWriter writer)
        {
        }
    }
}

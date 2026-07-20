using System;

namespace ZergRush.CodeGen
{
    public sealed class DataInfo
    {
        public Type type;
        public Type carrierType;
        public string baseAccess;
        public bool canBeNull;
        public bool sureIsNull;

        public string name => baseAccess;

        public DataInfo SetupIsCell()
        {
            if (type != null && (type.IsCell() || type.IsLivableSlot()))
            {
                type = type.FirstGenericArg();
                baseAccess += ".value";
            }
            else if (type != null && type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
                canBeNull = true;
            }
            return this;
        }

        public ZRData ToZRData()
        {
            var options = ZRDataOption.None;
            if (canBeNull) options |= ZRDataOption.CanBeNull;
            if (sureIsNull) options |= ZRDataOption.SureIsNull;
            if (type != null && type.IsNullable()) options |= ZRDataOption.IsNullable;
            return new ZRData(baseAccess, ZRType.FromSystemType(type), options);
        }
    }

    public static partial class CodeGen
    {
        public static void GenWriteValueToStream(MethodBuilder sink, DataInfo info, string stream,
            bool compatibilityMode)
        {
            GenWriteValueToStream(sink, info.ToZRData(), stream);
        }

        public static void GenReadValueFromStream(MethodBuilder sink, DataInfo info, string stream,
            bool pooled, bool needVar = false, bool readDataNodeFromId = false)
        {
            var declaredType = ZRType.FromSystemType(info.type);
            var carrier = ZRType.FromSystemType(info.carrierType ?? info.type);
            GenReadValueFromStream(sink, info.ToZRData(), declaredType, info.baseAccess, carrier,
                stream, pooled, needVar);
        }

        public static void WriteJsonValueStatement(MethodBuilder sink, DataInfo info, bool inList,
            bool compatibilityMode)
        {
            WriteJsonValueStatement(sink, info.ToZRData(), inList, info.name);
        }

        public static void ReadJsonValueStatement(MethodBuilder sink, DataInfo info,
            bool needCreateVar, bool useTempVar)
        {
            var declaredType = ZRType.FromSystemType(info.type);
            var carrier = ZRType.FromSystemType(info.carrierType ?? info.type);
            ReadJsonValueStatement(sink, info.ToZRData(), declaredType, info.baseAccess, carrier,
                needCreateVar, useTempVar);
        }
    }
}

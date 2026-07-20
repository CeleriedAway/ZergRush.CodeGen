namespace ZergRush.CodeGen
{
    public sealed class DataInfo
    {
        public ZRType type;
        public ZRType carrierType;
        public string baseAccess;
        public bool canBeNull;
        public bool sureIsNull;

        public string name => baseAccess;

        public DataInfo SetupIsCell()
        {
            if (type != null && type.IsCell())
            {
                var arguments = type.GetGenericArguments();
                if (arguments.Length == 1) type = arguments[0];
                baseAccess += ".value";
            }
            return this;
        }

        public ZRData ToZRData()
        {
            var options = ZRDataOption.None;
            if (canBeNull) options |= ZRDataOption.CanBeNull;
            if (sureIsNull) options |= ZRDataOption.SureIsNull;
            if (type != null && type.IsNullable()) options |= ZRDataOption.IsNullable;
            return new ZRData(baseAccess, type, options);
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
            var carrier = info.carrierType ?? info.type;
            GenReadValueFromStream(sink, info.ToZRData(), info.type, info.baseAccess, carrier,
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
            var carrier = info.carrierType ?? info.type;
            ReadJsonValueStatement(sink, info.ToZRData(), info.type, info.baseAccess, carrier,
                needCreateVar, useTempVar);
        }
    }
}

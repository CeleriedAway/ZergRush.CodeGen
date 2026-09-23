using System;
using System.Linq;
using Type = ZergRush.CodeGen.ZRType;
using ZergRush.Alive;
using Newtonsoft.Json;
using ZergRush.CodeGen;

namespace ZergRush.CodeGen
{
    public static partial class CodeGen
    {
        public static string JsonWriteFuncName = "WriteJson";
        public static string JsonReadFuncName = "ReadFromJson";

        public static string readerClassName = "ZRJsonTextReader";
        public static string writerClassName = "ZRJsonTextWriter";

        static JsonTextWriter writer;
        static JsonTextReader reader;

        public static bool IsFix64(this Type type)
        {
            return type.Name == "Fix64";
        }

        public static bool IsGuid(this Type type)
        {
            return type == typeof(Guid) || type == typeof(Guid?);
        }
        public static bool IsDateTime(this Type type)
        {
            return type == typeof(DateTime);
        }

        public static void WriteJsonValueStatement(MethodBuilder sink, ZRData info, bool inList,
            string propertyName = null)
        {
            var t = info.Type;
            var valueAccess = info.ReadAccess;

            if (t.IsConfig() == false) RequestGen(t, sink.classType, GenTaskFlags.JsonSerialization);


            if (info.CanBeNull)
            {
                sink.content($"if (!({info.HasValueExpression}))");
                sink.openBrace();
                if (!inList) sink.content($"writer.WritePropertyName(\"{propertyName}\");");
                sink.content($"writer.WriteNull();");
                sink.closeBrace();
                sink.content($"else");
                sink.openBrace();
            }

            if (!inList) sink.content($"writer.WritePropertyName(\"{propertyName}\");");


            if (t.IsFix64())
            {
                sink.content($"writer.WriteValue({valueAccess}.RawValue);");
            }
            else if (t == typeof(DateTime))
            {
                sink.content($"writer.WriteValue({valueAccess}.Ticks);");
            }
            else if (t == typeof(Guid))
            {
                sink.content($"writer.WriteValue({valueAccess}.ToString());");
            }
            else if (t == typeof(byte[]))
            {
                sink.content($"writer.WriteValue({valueAccess}.ToBase64());");
            }
            else if (t.IsConfig() && !info.InsideConfigStorage)
            {
                sink.content($"writer.WriteValue({valueAccess}.{UIdFuncName}().ToString());");
            }
            else if (t == typeof(ulong))
            {
                sink.content($"writer.WriteValue({valueAccess}.ToString());");
            }
            else if (t == typeof(decimal))
            {
                sink.content($"writer.WriteValue({valueAccess}.ToString(System.Globalization.CultureInfo.InvariantCulture));");
            }
            else if (t == typeof(Guid))
            {
                sink.content($"writer.WriteValue({valueAccess}.ToString());");
            }
            else if (t.IsPrimitive || t.IsString())
            {
                sink.content($"writer.WriteValue({valueAccess});");
            }
            else if (t.IsEnum)
            {
                sink.content($"writer.WriteValue({valueAccess}.ToString());");
            }
            else if (t.IsMultipleReference())
            {
                sink.content($"writer.WriteObjectWithRef({valueAccess});");
            }
            else
            {
                sink.content(SerializationCall(t, GenTaskFlags.JsonSerialization, JsonWriteFuncName, valueAccess, "writer") + ";");
            }

            if (info.CanBeNull)
            {
                sink.closeBrace();
            }
        }

        public static bool IsConfig(this Type t)
        {
            return t.IsChildOf<LoadableConfig>();
        }

        public static void ReadJsonValueStatement(MethodBuilder sink, ZRData info, Type declaredType,
            string declaredAccess, Type carrierType, bool needCreateVar, bool useTempVar = false)
        {
            var t = info.Type;
            if (t.IsConfig() == false) RequestGen(t, sink.classType, GenTaskFlags.JsonSerialization);

            // info can be transformed because read from can do temp value wrapping for it
            Action<MethodBuilder, ZRData> baseCall = (s, info1) =>
                s.content(
                    (info.Type.IsArray ? info1.Access + " = " : "") + SerializationCall(t, GenTaskFlags.JsonSerialization, JsonReadFuncName, info1.Access, "reader") + ";");
            if (t.IsMultipleReference())
            {
                baseCall = (s, info1) => s.content($"reader.ReadFromRef(ref {info1.Access});");
            }
            if ((t.IsValueType && t.IsControllable() == false) || t == typeof(byte[]) || t.IsGuid())
            {
                baseCall = (s, info1) => s.content($"{info1.Access} = {DirectJsonImmutableTypeReader(t)};");
            }
            else if (t != typeof(byte[]) && t.IsImmutableType())
            {
                baseCall = (s, info1) => s.content($"{info1.Access} = ({t.RealName()}) reader.Value;");
            }

            if (t.IsFix64() || (t.IsNullable() && t.NullableUnderlyingType()?.Name == "Fix64"))
            {
                sink.classBuilder.usingSink("FixMath.NET");
            }

            GeneralReadFrom(sink, info, declaredType, declaredAccess, carrierType,
                baseReadCall: baseCall,
                isNullReader: $"reader.TokenType == JsonToken.Null",
                pooled: false,
                classIdReader: $"reader.ReadJsonClassId()",
                configIdReader: DirectJsonImmutableTypeReader,
                refInst: "",
                directReader: $"{DirectJsonImmutableTypeReader(t)}",
                needCreateVar: needCreateVar,
                useTempVarThenAssign: useTempVar || declaredType.HasDataWrapper() &&
                (info.Type.IsControllableStruct() || info.Type.IsMultipleReference())
            );
        }


        public static string DirectJsonImmutableTypeReader(Type t)
        {
            var str = $"({t.RealName(true)})";
            if (t.IsNullable())
            {
                t = t.NullableUnderlyingType();
            }

            if (t.IsGuid())
                return $"Guid.Parse((string)reader.Value)";
            if (t == typeof(DateTime))
                return $"new DateTime((Int64)reader.Value)";
            if (t == typeof(char))
                return "char.Parse((string)reader.Value)";
            if (t == typeof(decimal))
                return "Convert.ToDecimal(reader.Value, System.Globalization.CultureInfo.InvariantCulture)";
            if (t.IsEnum)
                return $"System.Enum.Parse<{t.RealName(true)}>((string)reader.Value)";
            if (t.Name == "Fix64")
                return $"Fix64.FromRaw((Int64)reader.Value)";
            if (t == typeof(bool))
                return str + $"reader.Value";
            // if (t.IsNullable())
            // {
            //     var valtype = Nullable.GetUnderlyingType(t);
            //     var cast = valtype == typeof(float) || valtype == typeof(double) ? "double" : "Int64";
            //     return $"(reader.Value == null ? ({valtype.RealName()})({cast})reader.Value : ({t.RealName()})null)";
            // }
            if (t == typeof(float))
                return "CodeGenImplTools.ReadJsonFloat(reader)";
            if (t == typeof(double))
                return "reader.ReadJsonDouble()";
            if (t == typeof(ulong))
                return $"ulong.Parse((string)reader.Value)";
            if (t.IsPrimitive)
                return str + $"(Int64)reader.Value";
            if (t == typeof(string))
                return str + $"reader.Value";
            if (t == typeof(byte[]))
                return $"((string)reader.Value).FromBase64()";
            return str + $"reader.ReadFromJson{t.UniqueName()}()";
        }

        static void JsonAssertReadStartStatement(MethodBuilder sink, string condition)
        {
            sink.content($"if ({condition}) throw new JsonSerializationException(\"Bad Json Format\");");
        }


        static void GenerateJsonSerialization(Type type, string funcPrefix)
        {
            var classSink = GenClassSink(type);
            classSink.usingSink("System.IO");
            classSink.usingSink("Newtonsoft.Json");

            const string writerName = "writer";
            const string readerName = "reader";

            var retType = typeof(bool);

            var accessPrefix = type.AccessPrefixInGeneratedFunction();
            if (type.IsList() || type.IsArray)
            {
                var returnTypeForReadMethod = type.IsArray ? type : retType;
                var sinkReader = MakeGenMethod(type, GenTaskFlags.JsonSerialization, funcPrefix + JsonReadFuncName,
                    returnTypeForReadMethod, $"{readerClassName} {readerName}");
                var sinkWriter = MakeGenMethod(type, GenTaskFlags.JsonSerialization, funcPrefix + JsonWriteFuncName,
                    Void, $"{writerClassName} {writerName}");

                var elemType = type.FirstGenericArg();
                if (elemType.IsConfig() == false) RequestGen(elemType, type, GenTaskFlags.JsonSerialization);
                string count = type.IsList() ? "Count" : "Length";

                // Writer
                sinkWriter.content($"writer.WriteStartArray();");

                sinkWriter.content($"for (int i = 0; i < {accessPrefix}.{count}; i++)");
                sinkWriter.content($"{{");
                sinkWriter.indent++;
                var itemOptions = type.IsConfigStorage()
                    ? ZRDataOption.InsideConfigStorage
                    : ZRDataOption.None;
                if (elemType.IsClass) itemOptions |= ZRDataOption.CanBeNull;
                WriteJsonValueStatement(sinkWriter,
                    elemType.ToData($"{accessPrefix}[i]", itemOptions), true);
                sinkWriter.indent--;
                sinkWriter.content($"}}");
                sinkWriter.content($"writer.WriteEndArray();");

                // Reader
                JsonAssertReadStartStatement(sinkReader, $"reader.TokenType != JsonToken.StartArray");
                if (type.IsArray)
                    sinkReader.content($"var __items = new System.Collections.Generic.List<{elemType.RealName(true)}>();");
                else sinkReader.content($"{accessPrefix}.Clear();");
                if (type.IsLivableList())
                {
                    sinkReader.content($"var __previousUpdateMode = self.{updatemod};");
                    sinkReader.content($"self.{updatemod} = true;");
                    sinkReader.content("try");
                    sinkReader.openBrace();
                }
                var target = type.IsArray ? "__items" : accessPrefix;
                sinkReader.content("while (true)");
                sinkReader.openBrace();
                sinkReader.content("reader.ReadRequired();");
                sinkReader.content("if (reader.TokenType == JsonToken.EndArray) break;");
                sinkReader.content($"reader.Budget.ReserveElement<{elemType.RealName(true)}>({target}.Count + 1);");
                if (!elemType.IsValueType)
                    sinkReader.content($"if (reader.TokenType == JsonToken.Null) {{ {target}.Add(null); continue; }}");
                var valueOptions = ZRDataOption.SureIsNull;
                if (type.IsConfigStorage()) valueOptions |= ZRDataOption.InsideConfigStorage;
                // Livable elements need a slot before hierarchy propagation during their read.
                if (elemType.IsLivableNode() && !type.IsArray) sinkReader.content($"{target}.Add(null);");
                ReadJsonValueStatement(sinkReader, elemType.ToData("val", valueOptions), elemType, "val", type, true);
                if (elemType.IsLivableNode() && !type.IsArray)
                    sinkReader.content($"{target}[{target}.Count - 1] = val;");
                else sinkReader.content($"{target}.Add(val);");
                sinkReader.closeBrace();
                if (type.IsLivableList())
                {
                    sinkReader.closeBrace();
                    sinkReader.content($"finally {{ self.{updatemod} = __previousUpdateMode; }}");
                }
                sinkReader.content(type.IsArray ? "return __items.ToArray();" : "return true;");
            }
            else if (type.IsDictionary())
            {
                var sinkReader = MakeGenMethod(type, GenTaskFlags.JsonSerialization, funcPrefix + JsonReadFuncName,
                    retType, $"{readerClassName} {readerName}");
                var sinkWriter = MakeGenMethod(type, GenTaskFlags.JsonSerialization, funcPrefix + JsonWriteFuncName,
                    Void, $"{writerClassName} {writerName}");
                var keyType = type.FirstGenericArg();
                var valType = type.SecondGenericArg();
                RequestGen(keyType, type, GenTaskFlags.JsonSerialization);
                RequestGen(valType, type, GenTaskFlags.JsonSerialization);

                // Writer

                sinkWriter.content($"writer.WriteStartArray();");
                sinkWriter.content($"foreach (var item in {accessPrefix})");
                sinkWriter.openBrace();
                sinkWriter.content($"writer.WriteStartObject();");
                sinkWriter.content($"writer.WritePropertyName(\"key\");");
                WriteJsonValueStatement(sinkWriter,
                    keyType.ToData("item.Key", type.IsConfigStorage()
                        ? ZRDataOption.InsideConfigStorage
                        : ZRDataOption.None),
                    true);
                sinkWriter.content($"writer.WritePropertyName(\"value\");");
                WriteJsonValueStatement(sinkWriter,
                    valType.ToData("item.Value", (type.IsConfigStorage()
                        ? ZRDataOption.InsideConfigStorage
                        : ZRDataOption.None) | (valType.IsValueType ? ZRDataOption.None : ZRDataOption.CanBeNull)),
                    true);
                sinkWriter.content($"writer.WriteEndObject();");
                sinkWriter.closeBrace();
                sinkWriter.content($"writer.WriteEndArray();");

                // Reader
                JsonAssertReadStartStatement(sinkReader, $"reader.TokenType != JsonToken.StartArray");
                sinkReader.content($"{accessPrefix}.Clear();");
                sinkReader.content("while (true)");
                sinkReader.openBrace();
                sinkReader.content("reader.ReadRequired();");
                sinkReader.content("if (reader.TokenType == JsonToken.EndArray) { break; }");
                JsonAssertReadStartStatement(sinkReader, $"reader.TokenType != JsonToken.StartObject");
                sinkReader.content($"reader.Budget.ReserveElement<{keyType.RealName(true)}>({accessPrefix}.Count + 1);");
                sinkReader.content($"reader.Budget.ReserveElement<{valType.RealName(true)}>({accessPrefix}.Count + 1);");
                sinkReader.content("reader.ReadRequired();");
                sinkReader.content("reader.RequireToken(JsonToken.PropertyName);");
                sinkReader.content("if ((string)reader.Value != \"key\") throw new JsonSerializationException(\"Expected dictionary key.\");");
                sinkReader.content("reader.ReadRequired();"); // key content
                var keyOptions = ZRDataOption.SureIsNull;
                if (type.IsConfigStorage()) keyOptions |= ZRDataOption.InsideConfigStorage;
                ReadJsonValueStatement(sinkReader, keyType.ToData("key", keyOptions),
                    keyType, "key", type, true);
                sinkReader.content("reader.ReadRequired();");
                sinkReader.content("reader.RequireToken(JsonToken.PropertyName);");
                sinkReader.content("if ((string)reader.Value != \"value\") throw new JsonSerializationException(\"Expected dictionary value.\");");
                sinkReader.content("reader.ReadRequired();"); // val content
                var valOptions = ZRDataOption.SureIsNull | (valType.IsValueType ? ZRDataOption.None : ZRDataOption.CanBeNull);
                if (type.IsConfigStorage()) valOptions |= ZRDataOption.InsideConfigStorage;
                ReadJsonValueStatement(sinkReader, valType.ToData("val", valOptions),
                    valType, "val", type, true);
                sinkReader.content("reader.ReadRequired();");
                sinkReader.content("reader.RequireToken(JsonToken.EndObject);"); // end keyval obj
                sinkReader.content($"{accessPrefix}.Add(key, val);");
                sinkReader.closeBrace();
                sinkReader.content("return true;");
            }
            else
            {
                bool externalMode = !type.IsControllable() || type.IsStruct();
                bool immutableMode = type.IsStruct() && !type.IsControllable();

                string readerFuncName = funcPrefix + JsonReadFuncName + (!externalMode ? "Field" : "") +
                                        (immutableMode ? type.UniqueName() : "");
                var sinkReader = MakeGenMethod(type, GenTaskFlags.JsonSerialization, readerFuncName,
                    immutableMode ? type : retType,
                    $"{(immutableMode ? "this " : "")}{readerClassName} {readerName}{(!externalMode ? ", string __name" : "")}",
                    immutableMode);
                var sinkWriter = MakeGenMethod(type, GenTaskFlags.JsonSerialization,
                    funcPrefix + JsonWriteFuncName + (!externalMode ? "Fields" : ""), Void,
                    $"{writerClassName} {writerName}");

                if (sinkReader.needBaseValCall)
                {
                    sinkReader.content($"if (base.{JsonReadFuncName}Field({readerName}, __name)) return true;");
                    sinkReader.needBaseValCall = false;
                }

                if (immutableMode) sinkReader.content($"var self = default({type.RealName(true)});");

                if (externalMode)
                {
                    sinkReader.content("reader.RequireToken(JsonToken.StartObject);");
                    sinkReader.content("while (true)");
                    sinkReader.openBrace();
                    sinkReader.content("reader.ReadRequired();");
                    sinkReader.content($"if (reader.TokenType == JsonToken.PropertyName)");
                    sinkReader.openBrace();
                    sinkReader.content($"var __name = (string) reader.Value;");
                    sinkReader.content("reader.ReadRequired();");
                    sinkWriter.content($"writer.WriteStartObject();");
                }

                type.ProcessMembers(GenTaskFlags.JsonSerialization, true,
                    (member, info, _) => { WriteJsonValueStatement(sinkWriter, info, false, member.Name); },
                    GenericMembers(sinkWriter));

                if (type.IsControllable() && type.IsValueType == false)
                    sinkReader.classBuilder.inheritance("IJsonSerializable");
                var hasGenericJsonMembers = type.IsGenericTypeDecl() &&
                                            type.GetMembersForCodeGen(GenTaskFlags.JsonSerialization)
                                                .Any(MemberDependsOnGenericParameter);
                var genericReaderBranchesStarted = false;
                var readerOptions = GenericMembers(sinkReader);
                if (hasGenericJsonMembers)
                {
                    readerOptions.beforeGenericBranches = () =>
                    {
                        sinkReader.content("default: break;");
                        sinkReader.closeBrace();
                        genericReaderBranchesStarted = true;
                    };
                    readerOptions.beginGenericBranch = _ =>
                    {
                        sinkReader.content("switch(__name)");
                        sinkReader.openBrace();
                    };
                    readerOptions.endGenericBranch = _ =>
                    {
                        sinkReader.content(externalMode ? "default: break;" : "default: return false;");
                        sinkReader.closeBrace();
                    };
                }

                sinkReader.content($"switch(__name)");
                sinkReader.openBrace();
                type.ProcessMembers(GenTaskFlags.JsonSerialization, true, (member, info, declaredAccess) =>
                {
                    sinkReader.content($"case \"{member.Name}\":");
                    ReadJsonValueStatement(sinkReader, info, member.DeclaredType ?? info.Type,
                        declaredAccess, type, false);
                    sinkReader.content(hasGenericJsonMembers && !externalMode ? "return true;" : "break;");
                }, readerOptions);
                if (!genericReaderBranchesStarted)
                {
                    if (externalMode) sinkReader.content("default: reader.SkipObj(); break;");
                    else if (!immutableMode) sinkReader.content($"default: return false; break;");
                    sinkReader.closeBrace();
                }

                if (externalMode)
                {
                    sinkReader.closeBrace();
                    sinkReader.content("else if (reader.TokenType == JsonToken.EndObject) { break; }");
                    sinkReader.content("else throw new JsonSerializationException(\"Expected object property.\");");
                    sinkReader.closeBrace();
                    sinkWriter.content($"writer.WriteEndObject();");
                }

                if (immutableMode) sinkReader.content("return self;");
                if (!immutableMode) sinkReader.content($"return true;");
            }
        }
    }
}

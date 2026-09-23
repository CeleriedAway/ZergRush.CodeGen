using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZergRush
{
    public static class JsonPersistence
    {
        /// <summary>Loads a fresh root. A migration operates on a detached JSON tree; failures never modify the input or an existing model.</summary>
        public static T Read<T>(string json, Func<JObject, JObject> migrate = null,
            DeserializationBudget budget = null, int maxInputCharacters = 16 * 1024 * 1024)
            where T : class, IJsonSerializable, new()
        {
            if (json == null) throw new ArgumentNullException(nameof(json));
            if (json.Length > maxInputCharacters) throw new JsonSerializationException("JSON input budget exceeded.");
            if (migrate != null)
            {
                var root = JObject.Parse(json, new JsonLoadSettings { DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error });
                var migrated = migrate((JObject)root.DeepClone()) ?? throw new JsonSerializationException("Migration returned no root.");
                json = migrated.ToString(Formatting.None);
                if (json.Length > maxInputCharacters) throw new JsonSerializationException("Migrated JSON input budget exceeded.");
            }
            var value = new T();
            using (var reader = new ZRJsonTextReader(new StringReader(json)))
            {
                if (budget != null) reader.Budget = budget;
                value.ReadRootFromJson(reader);
            }
            return value;
        }
    }

    public static class LegacySimpleListMigration
    {
        /// <summary>Count must come from authoritative application evidence, never null/default trimming or capacity.</summary>
        public static JArray FromValidatedCount(JObject legacy, int validatedCount)
        {
            if (legacy == null || legacy.Count != 1 || !(legacy["data"] is JArray storage) ||
                validatedCount < 0 || validatedCount > storage.Count)
                throw new JsonSerializationException("Invalid legacy SimpleList storage/count.");
            var result = new JArray();
            for (int i = 0; i < validatedCount; i++) result.Add(storage[i].DeepClone());
            return result;
        }

        /// <summary>Example root-level adapter for ranked reference entries. Validates every rank and ID before replacing the legacy field.</summary>
        public static JObject RankedEntries(JObject root, string listProperty, string ranksProperty, string idProperty)
        {
            var result = (JObject)root.DeepClone();
            if (result[listProperty] is JArray) return result;
            if (!(result[listProperty] is JObject legacy) || !(legacy["data"] is JArray storage) ||
                !(result[ranksProperty] is JArray ranks))
                throw new JsonSerializationException("Legacy ranked list needs a rank map and backing storage.");
            var seenRanks = new bool[ranks.Count];
            var seenIds = new System.Collections.Generic.HashSet<long>();
            foreach (var token in ranks)
            {
                if (!(token is JObject pair) || pair["key"]?.Type != JTokenType.Integer || pair["value"]?.Type != JTokenType.Integer)
                    throw new JsonSerializationException("Invalid rank map entry.");
                long id = (long)pair["key"];
                long rank = (long)pair["value"];
                if (rank < 0 || rank >= ranks.Count || rank >= storage.Count || seenRanks[(int)rank] || !seenIds.Add(id) ||
                    !(storage[(int)rank] is JObject entry) || entry[idProperty]?.Type != JTokenType.Integer || (long)entry[idProperty] != id)
                    throw new JsonSerializationException("Inconsistent legacy rank map; retain the original save for recovery.");
                seenRanks[(int)rank] = true;
            }
            result[listProperty] = FromValidatedCount(legacy, ranks.Count);
            return result;
        }
    }
}

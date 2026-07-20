using System.Collections.Generic;

namespace ZergRush.Alive
{
    public abstract class DataRoot
    {
        readonly Dictionary<int, object> gameEntities = new Dictionary<int, object>();

        public void Remember(object entity, int id)
        {
            if (id == 0) throw new ZergRushException($"zero id for entity {entity}");
            gameEntities[id] = entity;
        }

        public object Recall(int id)
        {
            return gameEntities[id];
        }

        public T Recall<T>(int id) where T : class
        {
            return Recall(id) as T;
        }

        public object RecallMayBe(int id)
        {
            if (id == 0) return null;
            gameEntities.TryGetValue(id, out var entity);
            return entity;
        }

        public T RecallMayBe<T>(int id) where T : class
        {
            return RecallMayBe(id) as T;
        }

        public void Forget(int id, object entity)
        {
            if (gameEntities.TryGetValue(id, out var stored) && ReferenceEquals(stored, entity))
                gameEntities.Remove(id);
        }
    }
}

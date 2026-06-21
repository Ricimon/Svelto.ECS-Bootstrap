using Svelto.DataStructures;
using Svelto.ECS;

namespace ECS
{
    public static class ECSGetComponentExtensions
    {
        public static ref T GetSingletonComponent<T>(this EntitiesDB entitiesDB, ExclusiveGroupStruct groupStructId)
            where T : unmanaged, IEntityComponent
        {
            return ref entitiesDB.QueryUniqueEntity<T>(groupStructId);
        }

        public static bool TryGetSingletonComponent<T>(this EntitiesDB entitiesDB, ExclusiveGroupStruct groupStructId, out T component)
            where T : unmanaged, IEntityComponent
        {
            var (t, count) = entitiesDB.QueryEntities<T>(groupStructId);
            if (count == 0)
            {
                component = default;
                return false;
            }
            component = t[0];
            return true;
        }

        public static ref T GetComponent<T>(this EntitiesDB entitiesDB, EGID egid)
            where T : unmanaged, IEntityComponent
        {
            return ref entitiesDB.QueryEntity<T>(egid);
        }

        public static ref T GetComponent<T>(this EntitiesDB entitiesDB, uint id, ExclusiveGroupStruct group)
            where T : unmanaged, IEntityComponent
        {
            return ref entitiesDB.QueryEntity<T>(id, group);
        }

        //
        // TryGetComponent, EGID input
        //
        public static bool TryGetComponent<T>(this EntitiesDB entitiesDB, EGID egid, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            if (entitiesDB.TryQueryEntitiesAndIndex(egid, out var i, out NB<T> t))
            {
                callback?.Invoke(ref t[i]);
                return true;
            }
            return false;
        }

        public static bool TryGetComponent<T1, T2>(this EntitiesDB entitiesDB, EGID egid, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2>(egid.groupID)
                .Each((uint i, ref T1 t1, ref T2 t2) =>
                {
                    if (i == egid.entityID)
                    {
                        callback?.Invoke(ref t1, ref t2);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        public static bool TryGetComponent<T1, T2, T3>(this EntitiesDB entitiesDB, EGID egid, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2, T3>(egid.groupID)
                .Each((uint i, ref T1 t1, ref T2 t2, ref T3 t3) =>
                {
                    if (i == egid.entityID)
                    {
                        callback?.Invoke(ref t1, ref t2, ref t3);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        public static bool TryGetComponent<T1, T2, T3, T4>(this EntitiesDB entitiesDB, EGID egid, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2, T3, T4>(egid.groupID)
                .Each((uint i, ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4) =>
                {
                    if (i == egid.entityID)
                    {
                        callback?.Invoke(ref t1, ref t2, ref t3, ref t4);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        //
        // TryGetComponent, id & ExclusiveGroup input
        //
        public static bool TryGetComponent<T>(this EntitiesDB entitiesDB, uint id, ExclusiveGroupStruct group, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            return entitiesDB.TryGetComponent(new EGID(id, group), callback);
        }

        public static bool TryGetComponent<T1, T2>(this EntitiesDB entitiesDB, uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            return entitiesDB.TryGetComponent(new EGID(id, group), callback);
        }

        public static bool TryGetComponent<T1, T2, T3>(this EntitiesDB entitiesDB, uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            return entitiesDB.TryGetComponent(new EGID(id, group), callback);
        }

        public static bool TryGetComponent<T1, T2, T3, T4>(this EntitiesDB entitiesDB, uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            return entitiesDB.TryGetComponent(new EGID(id, group), callback);
        }

        //
        // TryGetComponent, Groups input
        //
        public static bool TryGetComponent<T>(this EntitiesDB entitiesDB, uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T>(groups)
                .Each((EGID egid, ref T t) =>
                {
                    if (egid.entityID == id)
                    {
                        callback?.Invoke(ref t);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        public static bool TryGetComponent<T1, T2>(this EntitiesDB entitiesDB, uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2>(groups)
                .Each((EGID egid, ref T1 t1, ref T2 t2) =>
                {
                    if (egid.entityID == id)
                    {
                        callback?.Invoke(ref t1, ref t2);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        public static bool TryGetComponent<T1, T2, T3>(this EntitiesDB entitiesDB, uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2, T3>(groups)
                .Each((EGID egid, ref T1 t1, ref T2 t2, ref T3 t3) =>
                {
                    if (egid.entityID == id)
                    {
                        callback?.Invoke(ref t1, ref t2, ref t3);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }

        public static bool TryGetComponent<T1, T2, T3, T4>(this EntitiesDB entitiesDB, uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            bool foundEntity = false;
            entitiesDB.QueryEntities<T1, T2, T3, T4>(groups)
                .Each((EGID egid, ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4) =>
                {
                    if (egid.entityID == id)
                    {
                        callback?.Invoke(ref t1, ref t2, ref t3, ref t4);
                        foundEntity = true;
                    }
                });
            return foundEntity;
        }
    }
}
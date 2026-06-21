using Svelto.ECS;

namespace ECS
{
    public static class ECSQueryEachExtensions
    {
        //
        // Entity Collection
        //
        public static void Each<T>(this in EntityCollection<T> entities, (uint start, uint end) rangeOfEntities, QueryIndexCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            var (t1, id, count) = entities;
            for (var i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
            {
                callback?.Invoke(id[i], ref t1[i]);
            }
        }

        public static void Each<T>(this in EntityCollection<T> entities, QueryIndexCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            var (t1, id, count) = entities;
            for (var i = 0; i < count; i++)
            {
                callback?.Invoke(id[i], ref t1[i]);
            }
        }

        public static void Each<T1, T2>(this in EntityCollection<T1, T2> entities, QueryIndexCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            var (t1, t2, id, count) = entities;
            for (var i = 0; i < count; i++)
            {
                callback?.Invoke(id[i], ref t1[i], ref t2[i]);
            }
        }

        public static void Each<T1, T2, T3>(this in EntityCollection<T1, T2, T3> entities, QueryIndexCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            var (t1, t2, t3, id, count) = entities;
            for (var i = 0; i < count; i++)
            {
                callback?.Invoke(id[i], ref t1[i], ref t2[i], ref t3[i]);
            }
        }

        public static void Each<T1, T2, T3, T4>(this in EntityCollection<T1, T2, T3, T4> entities, QueryIndexCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            var (t1, t2, t3, t4, id, count) = entities;
            for (var i = 0; i < count; i++)
            {
                callback?.Invoke(id[i], ref t1[i], ref t2[i], ref t3[i], ref t4[i]);
            }
        }

        public static void Each<T>(this in EntityCollection<T> entities, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            entities.Each((uint _, ref T t) => callback?.Invoke(ref t));
        }

        public static void Each<T1, T2>(this in EntityCollection<T1, T2> entities, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            entities.Each((uint _, ref T1 t1, ref T2 t2) => callback?.Invoke(ref t1, ref t2));
        }

        public static void Each<T1, T2, T3>(this in EntityCollection<T1, T2, T3> entities, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            entities.Each((uint _, ref T1 t1, ref T2 t2, ref T3 t3) => callback?.Invoke(ref t1, ref t2, ref t3));
        }

        public static void Each<T1, T2, T3, T4>(this in EntityCollection<T1, T2, T3, T4> entities, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            entities.Each((uint _, ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4) => callback?.Invoke(ref t1, ref t2, ref t3, ref t4));
        }

        //
        // GroupsEnumerable
        //
        public static void Each<T>(this in GroupsEnumerable<T> entities, QueryEgidCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            foreach (var ((t, id, count), group) in entities)
            {
                for (var i = 0; i < count; i++)
                {
                    callback?.Invoke(new(id[i], group), ref t[i]);
                }
            }
        }

        public static void Each<T1, T2>(this in GroupsEnumerable<T1, T2> entities, QueryEgidCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            foreach (var ((t1, t2, id, count), group) in entities)
            {
                for (var i = 0; i < count; i++)
                {
                    callback?.Invoke(new(id[i], group), ref t1[i], ref t2[i]);
                }
            }
        }

        public static void Each<T1, T2, T3>(this in GroupsEnumerable<T1, T2, T3> entities, QueryEgidCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            foreach (var ((t1, t2, t3, id, count), group) in entities)
            {
                for (var i = 0; i < count; i++)
                {
                    callback?.Invoke(new(id[i], group), ref t1[i], ref t2[i], ref t3[i]);
                }
            }
        }

        public static void Each<T1, T2, T3, T4>(this in GroupsEnumerable<T1, T2, T3, T4> entities, QueryEgidCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            foreach (var ((t1, t2, t3, t4, id, count), group) in entities)
            {
                for (var i = 0; i < count; i++)
                {
                    callback?.Invoke(new(id[i], group), ref t1[i], ref t2[i], ref t3[i], ref t4[i]);
                }
            }
        }

        public static void Each<T>(this in GroupsEnumerable<T> entities, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            entities.Each((EGID _, ref T t) => callback?.Invoke(ref t));
        }

        public static void Each<T1, T2>(this in GroupsEnumerable<T1, T2> entities, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            entities.Each((EGID _, ref T1 t1, ref T2 t2) => callback?.Invoke(ref t1, ref t2));
        }

        public static void Each<T1, T2, T3>(this in GroupsEnumerable<T1, T2, T3> entities, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
        {
            entities.Each((EGID _, ref T1 t1, ref T2 t2, ref T3 t3) => callback?.Invoke(ref t1, ref t2, ref t3));
        }

        public static void Each<T1, T2, T3, T4>(this in GroupsEnumerable<T1, T2, T3, T4> entities, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
        {
            entities.Each((EGID _, ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4) => callback?.Invoke(ref t1, ref t2, ref t3, ref t4));
        }

        //
        // EntityFilterCollection
        //
        public static void Each<T>(this EntityFilterCollection filterCollection, EntitiesDB entitiesDB, QueryEgidCallback<T> callback)
            where T : unmanaged, IEntityComponent
        {
            foreach(var (fis, group) in filterCollection)
            {
                var (t, entityIds, _) = entitiesDB.QueryEntities<T>(group);
                for(var i = 0; i < fis.count; i++)
                {
                    var fi = fis[i];
                    callback?.Invoke(new(entityIds[i], group), ref t[fi]);
                }
            }
        }

        public static void Each<T1, T2>(this EntityFilterCollection filterCollection, EntitiesDB entitiesDB, QueryEgidCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
        {
            foreach(var (fis, group) in filterCollection)
            {
                var (t1, t2, entityIds, _) = entitiesDB.QueryEntities<T1, T2>(group);
                for(var i = 0; i < fis.count; i++)
                {
                    var fi = fis[i];
                    callback?.Invoke(new(entityIds[i], group), ref t1[fi], ref t2[fi]);
                }
            }
        }
    }
}
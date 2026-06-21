using System;
using System.Collections.Generic;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace ECS
{
    public class World : IDisposable
    {
        private class DbSystem : IQueryingEntitiesEngine
        {
            public EntitiesDB entitiesDB { get; set; }
            public void Ready() { }
        }

        public EntitiesDB EntitiesDB => dbSystem.entitiesDB;

        private readonly EnginesRoot enginesRoot;
        private readonly EntitiesSubmissionScheduler entitiesSubmissionScheduler;
        private readonly IEntityFactory entityFactory;
        private readonly IEntityFunctions entityFunctions;
        private readonly DbSystem dbSystem = new();
        private readonly List<ISystem> systems = new();
        private readonly IdPool idPool = new();

        public World()
        {
            entitiesSubmissionScheduler = new EntitiesSubmissionScheduler();
            enginesRoot = new(entitiesSubmissionScheduler);

            entityFactory = enginesRoot.GenerateEntityFactory();
            entityFunctions = enginesRoot.GenerateEntityFunctions();

            enginesRoot.AddEngine(dbSystem);
        }

        public void IsValid() => enginesRoot.IsValid();

        public void Dispose()
        {
            foreach (var system in systems)
            {
                if (system is IDisposable d)
                {
                    d.Dispose();
                }
            }
            enginesRoot.Dispose();
        }

        public void AddSystem(ISystem system)
        {
            system.World = this;
            enginesRoot.AddEngine(system);
            systems.Add(system);
        }

        public EntityInitializer Entity<T>(ExclusiveGroupStruct group) where T : IEntityDescriptor, new()
        {
            var id = idPool.Get();
            var initializer = entityFactory.BuildEntity<T>(new EGID(id, group));
            return initializer;
        }

        public EntityInitializer Entity<T>() where T : IEntityDescriptorAndGroup, new()
        {
            return Entity<T>(GroupHashMap.Map[typeof(T)]);
        }

        public void RemoveEntity<T>(EGID egid) where T : IEntityDescriptor, new()
        {
            entityFunctions.RemoveEntity<T>(egid);
        }

        public void RemoveEntity<T>(uint id, ExclusiveGroupStruct group) where T : IEntityDescriptor, new()
        {
            entityFunctions.RemoveEntity<T>(id, group);
        }

        public void RemoveEntity(EGID egid)
        {
            RemoveEntity<BaseEntityDescriptor>(egid);
        }

        public void RemoveEntity(uint id, ExclusiveGroupStruct group)
        {
            RemoveEntity<BaseEntityDescriptor>(id, group);
        }

        public void RemoveEntitiesFromGroup(ExclusiveGroupStruct group)
        {
            entityFunctions.RemoveEntitiesFromGroup(group);
        }

        public void Progress()
        {
            entitiesSubmissionScheduler.SubmitEntities();
            foreach (var system in systems)
            {
                system.Update();
            }
        }

        //
        // Functions lifted from EntitiesDB
        //
        public EntitiesDB.SveltoFilters GetFilters() => EntitiesDB.GetFilters();

        public EntityCollection<T> QueryComponents<T>(ExclusiveGroupStruct group)
            where T : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T>(group);

        public EntityCollection<T1, T2> QueryComponents<T1, T2>(ExclusiveGroupStruct group)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2>(group);

        public EntityCollection<T1, T2, T3> QueryComponents<T1, T2, T3>(ExclusiveGroupStruct group)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            where T3 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2, T3>(group);

        public EntityCollection<T1, T2, T3, T4> QueryComponents<T1, T2, T3, T4>(ExclusiveGroupStruct group)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            where T3 : struct, IEntityComponent
            where T4 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2, T3, T4>(group);

        public GroupsEnumerable<T> QueryComponents<T>(in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups)
            where T : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T>(groups);

        public GroupsEnumerable<T1, T2> QueryComponents<T1, T2>(in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2>(groups);

        public GroupsEnumerable<T1, T2, T3> QueryComponents<T1, T2, T3>(in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            where T3 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2, T3>(groups);

        public GroupsEnumerable<T1, T2, T3, T4> QueryComponents<T1, T2, T3, T4>(in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups)
            where T1 : struct, IEntityComponent
            where T2 : struct, IEntityComponent
            where T3 : struct, IEntityComponent
            where T4 : struct, IEntityComponent
            => EntitiesDB.QueryEntities<T1, T2, T3, T4>(groups);

        public T GetSingletonComponent<T>(ExclusiveGroupStruct group) where T : unmanaged, IEntityComponent
            => EntitiesDB.QueryUniqueEntity<T>(group);

        public bool TryGetSingletonComponent<T>(ExclusiveGroupStruct group, out T component) where T : unmanaged, IEntityComponent
            => EntitiesDB.TryGetSingletonComponent(group, out component);

        public ref T GetComponent<T>(EGID egid) where T : unmanaged, IEntityComponent
            => ref EntitiesDB.QueryEntity<T>(egid);

        public ref T GetComponent<T>(uint id, ExclusiveGroupStruct group) where T : unmanaged, IEntityComponent
            => ref EntitiesDB.QueryEntity<T>(id, group);

        public bool TryGetComponent<T>(EGID egid, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(egid, callback);

        public bool TryGetComponent<T1, T2>(EGID egid, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(egid, callback);

        public bool TryGetComponent<T1, T2, T3>(EGID egid, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(egid, callback);

        public bool TryGetComponent<T1, T2, T3, T4>(EGID egid, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(egid, callback);

        public bool TryGetComponent<T>(uint id, ExclusiveGroupStruct group, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, group, callback);

        public bool TryGetComponent<T1, T2>(uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, group, callback);

        public bool TryGetComponent<T1, T2, T3>(uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, group, callback);

        public bool TryGetComponent<T1, T2, T3, T4>(uint id, ExclusiveGroupStruct group, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, group, callback);

        public bool TryGetComponent<T>(uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T> callback)
            where T : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, groups, callback);

        public bool TryGetComponent<T1, T2>(uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, groups, callback);

        public bool TryGetComponent<T1, T2, T3>(uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2, T3> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, groups, callback);

        public bool TryGetComponent<T1, T2, T3, T4>(uint id, in LocalFasterReadOnlyList<ExclusiveGroupStruct> groups, QueryCallback<T1, T2, T3, T4> callback)
            where T1 : unmanaged, IEntityComponent
            where T2 : unmanaged, IEntityComponent
            where T3 : unmanaged, IEntityComponent
            where T4 : unmanaged, IEntityComponent
            => EntitiesDB.TryGetComponent(id, groups, callback);

        public uint QueryIndex<T>(EGID egid)
            where T : unmanaged, IEntityComponent
        {
            EntitiesDB.QueryEntitiesAndIndex<T>(egid, out var index);
            return index;
        }

        public uint QueryIndex<T>(uint id, ExclusiveGroupStruct group)
            where T : unmanaged, IEntityComponent
        {
            EntitiesDB.QueryEntitiesAndIndex<T>(id, group, out var index);
            return index;
        }

        public bool TryQueryIndex<T>(EGID egid, out uint index)
            where T : unmanaged, IEntityComponent
        {
            return EntitiesDB.TryQueryEntitiesAndIndex<T>(egid, out index, out _);
        }

        public bool TryQueryIndex<T>(uint id, ExclusiveGroupStruct group, out uint index)
            where T : unmanaged, IEntityComponent
        {
            return EntitiesDB.TryQueryEntitiesAndIndex<T>(id, group, out index, out _);
        }
    }
}
using System;
using Svelto.ECS;
using UnityEngine;

namespace ECS.Example
{
    public class ExampleSystem : ISystem, IReadySystem,
        IReactOnAddEx<ExampleComponent1>, IReactOnRemoveEx<ExampleComponent1>
    {
        public World World { get; set; }

        private EntitiesDB.SveltoFilters filters;

        public void Ready()
        {
            filters = World.GetFilters();
            filters.GetOrCreateTransientFilter<ExampleComponent1>(ExampleFilters.ExampleFilter);
        }

        public void Update()
        {
            var (components, entityIDs, count) = World.QueryComponents<ExampleComponent1>(ExampleEntity1.Group);
            var exampleFilter = filters.GetTransientFilter<ExampleComponent1>(ExampleFilters.ExampleFilter);
            Debug.Log($"ExampleSystem found {count} example entities, {exampleFilter.ComputeFinalCount()} filtered entities");

            World.QueryComponents<ExampleComponent2>(ExampleEntity2.Group)
                .Each((uint id, ref ExampleComponent2 _) =>
                {
                    Debug.Log("Removing example entity 2");
                    World.RemoveEntity(id, ExampleEntity2.Group);
                });
        }

        public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<ExampleComponent1> entities,
            ExclusiveGroupStruct groupID)
        {
            var count = rangeOfEntities.end - rangeOfEntities.start;
            Debug.Log($"ExampleSystem received event of {count} entities added");

            var exampleFilter = filters.GetTransientFilter<ExampleComponent1>(ExampleFilters.ExampleFilter);
            entities.Each(rangeOfEntities, (uint id, ref ExampleComponent1 ec) =>
            {
                if (ec.AddToFilter)
                {
                    var index = World.QueryIndex<ExampleComponent1>(id, groupID);
                    exampleFilter.Add(new EGID(id, groupID), index);
                }
            });
        }

        public void Remove((uint start, uint end) rangeOfEntities, in EntityCollection<ExampleComponent1> entities,
            ExclusiveGroupStruct groupID)
        {
            var count = rangeOfEntities.end - rangeOfEntities.start;
            Debug.Log($"ExampleSystem received event of {count} entities removed");
        }
    }
}
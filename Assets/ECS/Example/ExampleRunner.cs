using UnityEngine;

namespace ECS.Example
{
    public class ExampleRunner : MonoBehaviour
    {
        private World world;

        private void Start()
        {
            world = new World();

            world.AddSystem(new ExampleSystem());

            var entity1 = world.Entity<ExampleEntity1>();
            entity1.Init(new ExampleComponent1
            {
                AddToFilter = true,
            });

            var entity2 = world.Entity<ExampleEntity1>();
            entity2.Init(new ExampleComponent1
            {
                AddToFilter = false,
            });

            world.Entity<ExampleEntity2>();
        }

        private void Update()
        {
            world.Progress();
        }

        private void OnDestroy()
        {
            world.Dispose();
        }
    }
}
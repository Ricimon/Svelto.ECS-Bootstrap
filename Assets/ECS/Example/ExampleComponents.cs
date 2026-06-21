using Svelto.ECS;

namespace ECS.Example
{
    public struct ExampleComponent1 : IEntityComponent
    {
        public bool AddToFilter;
    }

    public struct ExampleComponent2 : IEntityComponent
    {
    }

    public class ExampleEntity1 : EntityDescriptorAndGroup<ExampleComponent1> {}
    public class ExampleEntity2 : EntityDescriptorAndGroup<ExampleComponent2> {}
}
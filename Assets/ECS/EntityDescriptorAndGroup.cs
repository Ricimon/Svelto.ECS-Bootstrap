using Svelto.ECS;

namespace ECS
{
    public interface IEntityDescriptorAndGroup : IEntityDescriptor { }

    public class EntityDescriptorAndGroup<T> : ExtendibleEntityDescriptor<BaseEntityDescriptor>, IEntityDescriptorAndGroup
        where T : struct, IEntityComponent
    {
        public static ExclusiveGroup Group = new();
        public EntityDescriptorAndGroup()
        {
            ExtendWith(new IComponentBuilder[]
            {
                new ComponentBuilder<T>()
            });
            GroupHashMap.Map.Add(GetType(), Group);
        }
    }

    public class EntityDescriptorAndGroup<T1, T2> : ExtendibleEntityDescriptor<BaseEntityDescriptor>
        where T1 : struct, IEntityComponent
        where T2 : struct, IEntityComponent
    {
        public static ExclusiveGroup Group = new();
        public EntityDescriptorAndGroup()
        {
            ExtendWith(new IComponentBuilder[]
            {
                new ComponentBuilder<T1>(),
                new ComponentBuilder<T2>(),
            });
            GroupHashMap.Map.Add(GetType(), Group);
        }
    }

    public class EntityDescriptorAndGroup<T1, T2, T3> : ExtendibleEntityDescriptor<BaseEntityDescriptor>
        where T1 : struct, IEntityComponent
        where T2 : struct, IEntityComponent
        where T3 : struct, IEntityComponent
    {
        public static ExclusiveGroup Group = new();
        public EntityDescriptorAndGroup()
        {
            ExtendWith(new IComponentBuilder[]
            {
                new ComponentBuilder<T1>(),
                new ComponentBuilder<T2>(),
                new ComponentBuilder<T3>(),
            });
            GroupHashMap.Map.Add(GetType(), Group);
        }
    }

    public class EntityDescriptorAndGroup<T1, T2, T3, T4> : ExtendibleEntityDescriptor<BaseEntityDescriptor>
        where T1 : struct, IEntityComponent
        where T2 : struct, IEntityComponent
        where T3 : struct, IEntityComponent
        where T4 : struct, IEntityComponent
    {
        public static ExclusiveGroup Group = new();
        public EntityDescriptorAndGroup()
        {
            ExtendWith(new IComponentBuilder[]
            {
                new ComponentBuilder<T1>(),
                new ComponentBuilder<T2>(),
                new ComponentBuilder<T3>(),
                new ComponentBuilder<T4>(),
            });
            GroupHashMap.Map.Add(GetType(), Group);
        }
    }

    public class EntityDescriptorAndGroup<T1, T2, T3, T4, T5> : ExtendibleEntityDescriptor<BaseEntityDescriptor>
        where T1 : struct, IEntityComponent
        where T2 : struct, IEntityComponent
        where T3 : struct, IEntityComponent
        where T4 : struct, IEntityComponent
        where T5 : struct, IEntityComponent
    {
        public static ExclusiveGroup Group = new();
        public EntityDescriptorAndGroup()
        {
            ExtendWith(new IComponentBuilder[]
            {
                new ComponentBuilder<T1>(),
                new ComponentBuilder<T2>(),
                new ComponentBuilder<T3>(),
                new ComponentBuilder<T4>(),
                new ComponentBuilder<T5>(),
            });
            GroupHashMap.Map.Add(GetType(), Group);
        }
    }
}
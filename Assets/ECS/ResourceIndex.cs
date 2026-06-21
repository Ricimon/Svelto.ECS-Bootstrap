using Svelto.DataStructures.Experimental;

namespace ECS
{
    public struct ResourceIndex<T> where T : class
    {
        public ValueIndex ValueIndex;

        public ResourceIndex(ValueIndex valueIndex)
        {
            ValueIndex = valueIndex;
        }

        public readonly T ToObject(ResourceManagers resourceManagers)
        {
            return resourceManagers.Get<T>(ValueIndex);
        }

        public static implicit operator ValueIndex(ResourceIndex<T> ri) => ri.ValueIndex;
        public static implicit operator ResourceIndex<T>(ValueIndex vi) => new(vi);
    }
}
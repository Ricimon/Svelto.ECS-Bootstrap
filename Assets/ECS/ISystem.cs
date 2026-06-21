using Svelto.ECS;

namespace ECS
{
    public interface ISystem : IEngine
    {
        public World World { set; }

        public void Update();
    }

    public interface IReadySystem : IGetReadyEngine { }
}
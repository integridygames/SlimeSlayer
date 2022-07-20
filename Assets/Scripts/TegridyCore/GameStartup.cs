using JetBrains.Annotations;
using Zenject;

namespace TegridyCore
{
    [UsedImplicitly]
    public class GameStartup : IInitializable, ITickable, IFixedTickable
    {
        private readonly SystemManager _systemManager;

        public GameStartup(SystemManager systemManager)
        {
            _systemManager = systemManager;
        }

        public void Initialize()
        {
            _systemManager.PreInitialize();
            _systemManager.Initialize();
            _systemManager.StartCoroutines();
        }

        public void Tick()
        {
            _systemManager.Update();
        }

        public void FixedTick()
        {
            _systemManager.FixedUpdate();
        }
    }
}
using Game.Gameplay.Services;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Level
{
    public class LevelDestroySystem : IDestroySystem
    {
        private readonly LevelDestroyService _levelDestroyService;

        public LevelDestroySystem(LevelDestroyService levelDestroyService)
        {
            _levelDestroyService = levelDestroyService;
        }

        public void Destroy()
        {
            _levelDestroyService.DestroyLevel();
        }
    }
}
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Level;
using JetBrains.Annotations;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Level
{
    [UsedImplicitly]
    public class LevelInitializeSystem : IPreInitializeSystem
    {
        private readonly LevelFactory _levelFactory;
        private readonly LevelInfo _levelInfo;      

        public LevelInitializeSystem(LevelFactory levelFactory,
            LevelInfo levelInfo)
        {
            _levelFactory = levelFactory;
            _levelInfo = levelInfo;         
        }

        public void PreInitialize()
        {
            var levelView = _levelFactory.Create();
            _levelInfo.CurrentLevelView.Value = levelView;

            _levelFactory.NextLevel(false);
        }
    }
}
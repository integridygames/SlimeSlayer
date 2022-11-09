using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Enemy;
using System.Collections.Generic;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Enemy 
{
    public class EnemiesInitializeSystem : IInitializeSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly LevelInfo _levelInfo;

        private List<EnemyViewBase> _enemiesViews;

       public EnemiesInitializeSystem(ActiveEnemiesContainer activeEnemiesContainer, LevelInfo levelInfo) 
       {
            _activeEnemiesContainer = activeEnemiesContainer;
            _levelInfo = levelInfo;
       }

        public void Initialize()
        {

        }
    }
}
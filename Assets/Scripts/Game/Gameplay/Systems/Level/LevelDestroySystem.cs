using Game.Gameplay.Models.Level;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Level
{
    public class LevelDestroySystem : IDestroySystem
    {
        private readonly LevelInfo _levelInfo;

        public LevelDestroySystem(LevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
        }
        
        public void Destroy()
        {
            Object.Destroy(_levelInfo.CurrentLevelView.Value.gameObject);
        }
    }
}
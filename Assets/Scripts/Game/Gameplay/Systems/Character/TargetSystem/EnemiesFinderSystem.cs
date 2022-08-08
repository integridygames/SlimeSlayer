using Game.Gameplay.Views.Character;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Models.Character.TargetSystem;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem 
{
    public class EnemiesFinderSystem : IUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly float _radius = 30f;
        private TargetsInfo _targetsInfo;

        public Collider[] Targets { get; private set; }

        public EnemiesFinderSystem(CharacterView characterView, TargetsInfo targetsInfo)
        {
            _characterView = characterView;
            _targetsInfo = targetsInfo;
        }
        public void Update()
        {
            _targetsInfo.Targets = TryToFindEnemies();
        }

        private Collider[] TryToFindEnemies()
        {
            return Physics.OverlapSphere(_characterView.transform.position, _radius, (int)Layers.Enemy);
        }
    }
}
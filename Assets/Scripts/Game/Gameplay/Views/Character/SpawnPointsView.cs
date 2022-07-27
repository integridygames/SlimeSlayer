using UnityEngine;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Views.Character
{
    public class SpawnPointsView : ViewBase, IInitializable
    {
        private Transform[] _spawnPoints;
        private int _spawnPointsQuantity;

        public Transform[] SpawnPoints => _spawnPoints;
        public int SpawnPointsQuantity => _spawnPointsQuantity;

        public void Initialize()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
            _spawnPointsQuantity = _spawnPoints.Length;
        }
    }
}
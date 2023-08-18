using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Gameplay.Views.Zone;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        private SpawnPointView _spawnPointView;
        private GunCabinetView _gunCabinetView;
        private FinishView _finishView;
        private List<SpawnBoundsView> _spawnBoundsViews;

        [SerializeField] private Transform _spawnRoot;

        public SpawnPointView SpawnPointView => _spawnPointView ??= GetComponentInChildren<SpawnPointView>();
        public GunCabinetView GunCabinetView => _gunCabinetView ??= GetComponentInChildren<GunCabinetView>();
        public FinishView FinishView => _finishView ??= GetComponentInChildren<FinishView>();

        public IReadOnlyList<SpawnBoundsView> SpawnBoundsViews =>
            _spawnBoundsViews ??= GetComponentsInChildren<SpawnBoundsView>().ToList();

        public Transform SpawnRoot => _spawnRoot;
    }
}
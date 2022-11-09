using UnityEngine;
using Game.Gameplay.Views.Zone;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private SpawnPointView _spawnPointView;
        [SerializeField] private GunCabinetView _gunCabinetView;
        [SerializeField] private FinishView _finishView;
        [SerializeField] private ZoneView[] _zonesViews;

        public SpawnPointView SpawnPointView => _spawnPointView;
        public GunCabinetView GunCabinetView => _gunCabinetView;
        public FinishView FinishView => _finishView;
        public ZoneView[] ZonesViews => _zonesViews;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Views.Teleport;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        private SpawnPointView _spawnPointView;
        private GunCabinetView _gunCabinetView;
        private FinishView _finishView;
        private ZoneView[] _zonesViews;
        private BattlefieldZoneView[] _battlefieldZones;
        private TeleportView _teleportView;

        public SpawnPointView SpawnPointView => _spawnPointView ??= GetComponentInChildren<SpawnPointView>();
        public GunCabinetView GunCabinetView => _gunCabinetView ??= GetComponentInChildren<GunCabinetView>();
        public FinishView FinishView => _finishView ??= GetComponentInChildren<FinishView>();
        public ZoneView[] ZonesViews => _zonesViews ??= GetComponentsInChildren<ZoneView>();
        public TeleportView TeleportView => _teleportView ??= GetComponentInChildren<TeleportView>();
    }
}
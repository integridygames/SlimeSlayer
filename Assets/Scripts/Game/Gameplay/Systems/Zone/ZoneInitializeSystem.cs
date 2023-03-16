using System.Collections.Generic;
using TegridyCore.Base;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Models.Zone;
using System.Linq;
using Game.Gameplay.Models.Level;
using JetBrains.Annotations;

namespace Game.Gameplay.Systems.Zone
{
    [UsedImplicitly]
    public class ZonesInitializeSystem : IInitializeSystem
    {
        private const int MinimumZonesCount = 2;
        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;
        private readonly LevelInfo _levelInfo;

        public ZonesInitializeSystem(SpawnZonesDataContainer spawnZonesDataContainer, LevelInfo levelInfo)
        {
            _spawnZonesDataContainer = spawnZonesDataContainer;
            _levelInfo = levelInfo;
        }

        public void Initialize()
        {           
            var zoneViews = _levelInfo.CurrentLevelView.Value.SpawnBoundsViews.ToList();
            var zonesData = GetFilledZonesData(zoneViews);

            _spawnZonesDataContainer.InitializeZonesData(zonesData);
        }

        private static List<SpawnZoneData> GetFilledZonesData(IReadOnlyList<SpawnBoundsView> spawnBoundsViews)
        {
            var zonesData = new List<SpawnZoneData>(MinimumZonesCount);

            foreach (var spawnBoundsView in spawnBoundsViews)
            {
                zonesData.Add(new SpawnZoneData(spawnBoundsView));
            }

            return zonesData;
        }
    }
}
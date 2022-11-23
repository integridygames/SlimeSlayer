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
        private const int MinimumZonesCount = 3;
        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly LevelInfo _levelInfo;

        public ZonesInitializeSystem(ZonesDataContainer zonesDataContainer, LevelInfo levelInfo)
        {
            _zonesDataContainer = zonesDataContainer;
            _levelInfo = levelInfo;
        }

        public void Initialize()
        {           
            var zoneViews = _levelInfo.CurrentLevelView.Value.ZonesViews.ToList();

            var zonesData = GetFilledZonesData(zoneViews);

            _zonesDataContainer.InitializeZonesData(zonesData);
            _zonesDataContainer.SetCurrentZone(zonesData[0]);
        }

        private static List<ZoneData> GetFilledZonesData(List<ZoneView> zoneViews)
        {
            var zonesData = new List<ZoneData>(MinimumZonesCount);

            foreach (var zone in zoneViews)
            {
                var zoneData = zone is BattlefieldZoneView ? new BattlefieldZoneData(zone) : new ZoneData(zone);

                zonesData.Add(zoneData);
            }

            return zonesData;
        }
    }
}
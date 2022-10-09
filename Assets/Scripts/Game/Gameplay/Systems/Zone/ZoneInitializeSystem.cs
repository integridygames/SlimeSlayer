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
        private readonly ZonesInfo _zonesInfo;
        private readonly LevelInfo _levelInfo;

        public ZonesInitializeSystem(ZonesInfo zonesInfo, LevelInfo levelInfo)
        {
            _zonesInfo = zonesInfo;
            _levelInfo = levelInfo;
        }

        public void Initialize()
        {           
            var zoneViews = _levelInfo.CurrentLevelView.Value.ZonesViews.ToList();

            var zonesData = GetFilledZonesData(zoneViews);

            _zonesInfo.InitializeZonesDatas(zonesData);
            _zonesInfo.SetCurrentZone(zonesData[0]);
        }

        private List<ZoneData> GetFilledZonesData(List<ZoneView> zoneViews)
        {
            var zonesData = new List<ZoneData>(MinimumZonesCount);

            foreach (var zone in zoneViews)
            {
                var zoneData = new ZoneData(zone);
                zoneData.Initialize();
                zonesData.Add(zoneData);
            }

            return zonesData;
        }
    }
}
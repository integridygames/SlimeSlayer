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
        private readonly ZonesInfo _zonesInfo;
        private readonly LevelInfo _levelInfo;
        private readonly List<ZoneData> _zonesDatas;

        public ZonesInitializeSystem(ZonesInfo zonesInfo, LevelInfo levelInfo)
        {
            _zonesInfo = zonesInfo;
            _levelInfo = levelInfo;
            _zonesDatas = new List<ZoneData>();
        }

        public void Initialize()
        {
            var zones = _levelInfo.CurrentLevelView.Value.ZonesViews.ToList();

            InitializeZones(zones);

            _zonesInfo.InitializeZonesDatas(_zonesDatas);
            _zonesInfo.SetCurrentZone(_zonesDatas[0]);
        }

        private void InitializeZones(List<ZoneView> zones) 
        {
            foreach (var zone in zones)
            {
                ZoneData zoneData = new ZoneData(zone);
                zoneData.Initialize();
                _zonesDatas.Add(zoneData);
            }
        }
    }
}
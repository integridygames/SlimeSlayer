using System.Collections.Generic;
using TegridyCore.Base;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Models.Zone;

namespace Game.Gameplay.Systems.Zone 
{
    public class ZonesInitializeSystem : IInitializeSystem
    {
        private readonly ZonesInfo _zonesInfo;
        private readonly List<ZoneView> _zones;

        public ZonesInitializeSystem(ZonesInfo zonesInfo, List<ZoneView> zones) 
        {
            _zonesInfo = zonesInfo;
            _zones = zones;
        }

        public void Initialize()
        {
            _zonesInfo.InitializeZones(_zones);
            _zonesInfo.SetCurrentZone(null);

            foreach (var zone in _zonesInfo.Zones) 
            {
                zone.Initialize();
            }
        }
    }
}
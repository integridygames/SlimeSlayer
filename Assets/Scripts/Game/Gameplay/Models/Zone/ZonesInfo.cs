using Game.Gameplay.Views.Zone;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Zone 
{
    public class ZonesInfo
    {
        public List<ZoneView> Zones { get; private set; }

        public ZoneView CurrentZone { get; private set; }

        public void SetCurrentZone(ZoneView zoneView) 
        {
            CurrentZone = zoneView;
        }

        public void InitializeZones(List<ZoneView> zones) 
        {
            Zones = zones;
        }
    }
}
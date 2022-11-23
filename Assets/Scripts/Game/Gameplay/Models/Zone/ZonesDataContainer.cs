using System.Collections.Generic;

namespace Game.Gameplay.Models.Zone
{
    public class ZonesDataContainer
    {
        public List<ZoneData> ZonesData { get; private set; }
        public ZoneData CurrentZoneData { get; private set; }

        public void SetCurrentZone(ZoneData zoneData)
        {
            CurrentZoneData = zoneData;
        }

        public void InitializeZonesData(List<ZoneData> zonesData)
        {
            ZonesData = zonesData;
        }
    }
}
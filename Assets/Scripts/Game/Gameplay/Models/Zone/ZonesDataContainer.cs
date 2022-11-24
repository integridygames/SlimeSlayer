using System.Collections.Generic;
using System.Linq;

namespace Game.Gameplay.Models.Zone
{
    public class ZonesDataContainer
    {
        public Dictionary<int, ZoneData> ZonesData { get; private set; }
        public ZoneData CurrentZoneData { get; private set; }

        public void SetCurrentZone(ZoneData zoneData)
        {
            CurrentZoneData = zoneData;
        }

        public void InitializeZonesData(List<ZoneData> zonesData)
        {
            ZonesData = zonesData.ToDictionary(x => x.ZoneId);
        }
    }
}
using System.Collections.Generic;

namespace Game.Gameplay.Models.Zone
{
    public class ZonesInfo
    {
        public List<ZoneData> ZonesDatas { get; private set; }
        public ZoneData CurrentZoneData { get; private set; }

        public void SetCurrentZone(ZoneData zoneData)
        {
            CurrentZoneData = zoneData;
        }

        public void InitializeZonesDatas(List<ZoneData> zonesDatas)
        {
            ZonesDatas = zonesDatas;
        }
    }
}
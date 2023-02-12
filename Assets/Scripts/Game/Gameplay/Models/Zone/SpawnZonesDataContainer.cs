using System.Collections.Generic;

namespace Game.Gameplay.Models.Zone
{
    public class SpawnZonesDataContainer
    {
        public List<SpawnZoneData> SpawnZonesData { get; private set; }

        public void InitializeZonesData(List<SpawnZoneData> zonesData)
        {
            SpawnZonesData = zonesData;
        }
    }
}
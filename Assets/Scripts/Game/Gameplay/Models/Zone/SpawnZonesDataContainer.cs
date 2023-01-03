using System.Collections.Generic;
using System.Linq;

namespace Game.Gameplay.Models.Zone
{
    public class SpawnZonesDataContainer
    {
        public Dictionary<int, SpawnZoneData> SpawnZonesData { get; private set; }

        public void InitializeZonesData(List<SpawnZoneData> zonesData)
        {
            SpawnZonesData = zonesData.ToDictionary(x => x.SpawnZoneId);
        }
    }
}
using Game.Gameplay.Models.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class SpawnTimeoutSystem : IUpdateSystem
    {
        private readonly SpawnZonesDataContainer _zonesDataContainer;

        public SpawnTimeoutSystem(SpawnZonesDataContainer zonesDataContainer)
        {
            _zonesDataContainer = zonesDataContainer;
        }

        public void Update()
        {
            foreach (var zoneData in _zonesDataContainer.SpawnZonesData)
            {
                var nextTimeoutValue = zoneData.CurrentTimeout - Time.deltaTime;

                zoneData.CurrentTimeout = Mathf.Clamp(nextTimeoutValue, 0, SpawnZoneData.EnemyRespawnTime);
            }
        }
    }
}
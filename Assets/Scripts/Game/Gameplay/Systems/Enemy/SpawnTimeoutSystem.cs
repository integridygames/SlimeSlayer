using Game.Gameplay.Models.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class SpawnTimeoutSystem : IUpdateSystem
    {
        private readonly ZonesDataContainer _zonesDataContainer;

        public SpawnTimeoutSystem(ZonesDataContainer zonesDataContainer)
        {
            _zonesDataContainer = zonesDataContainer;
        }

        public void Update()
        {
            foreach (var zoneData in _zonesDataContainer.ZonesData.Values)
            {
                if (zoneData is not BattlefieldZoneData battlefieldZoneData) continue;

                var nextTimeoutValue = battlefieldZoneData.CurrentTimeout - Time.deltaTime;

                battlefieldZoneData.CurrentTimeout = Mathf.Clamp(nextTimeoutValue, 0, BattlefieldZoneData.EnemyRespawnTime);
            }
        }
    }
}
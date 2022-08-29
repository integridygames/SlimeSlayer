using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy 
{
    public class EnemyPatrolSystem : IUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;

        private const float Inaccuracy = 0.1f;

        public EnemyPatrolSystem(ZonesInfo zonesInfo) 
        {
            _zonesInfo = zonesInfo;
        }

        public void Update()
        {
            foreach(var zone in _zonesInfo.Zones) 
            {
                if (!CheckIfZoneIsTriggered(zone)) 
                {
                    TryToSetEnemiesPatrolPoints(zone);
                }
            }
        }

        private bool CheckIfZoneIsTriggered(ZoneView zone) 
        {
            return zone.IsZoneTriggered;
        }

        private void TryToSetEnemiesPatrolPoints(ZoneView zone) 
        {
            foreach (EnemyView enemy in zone.EnemiesPool)
            {
                TryToSetPatrolPoint(enemy, zone);
            }
        }

        private void TryToSetPatrolPoint(EnemyView enemy, ZoneView zone) 
        {
            if (CheckIfPointIsReached(enemy) && enemy.gameObject.activeInHierarchy)
            {
                Vector2 point = zone.GetRandomPoint();
                enemy.SetPatrolPoint(new Vector3(point.x, enemy.transform.position.y, point.y));
            }
        }

        private bool CheckIfPointIsReached(EnemyView enemy) 
        {
            return Vector3.Distance(enemy.transform.position, enemy.CurrentPatrolPoint) <= Inaccuracy;
        }
    }
}
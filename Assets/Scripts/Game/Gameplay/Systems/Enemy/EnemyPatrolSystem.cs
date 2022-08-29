using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy 
{
    public class EnemyPatrolSystem : IUpdateSystem
    {
        private readonly List<ZoneView> _zones;

        private const float Inaccuracy = 0.1f;

        public EnemyPatrolSystem(List<ZoneView> zones) 
        {
            _zones = zones;
        }

        public void Update()
        {
            foreach(var zone in _zones) 
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
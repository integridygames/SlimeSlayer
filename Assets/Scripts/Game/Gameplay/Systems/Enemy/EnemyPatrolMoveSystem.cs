using System.Collections.Generic;
using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.Systems.Enemy 
{  
    public class EnemyPatrolMoveSystem : IFixedUpdateSystem
    {
        private readonly List<ZoneView> _zones;

        public const float Inaccuracy = 0.1f;

        public EnemyPatrolMoveSystem(List<ZoneView> zones)
        {
            _zones = zones;
        }

        public void FixedUpdate()
        {
            foreach (var zone in _zones)
            {
                if (!CheckIfZoneIsTriggered(zone))
                {
                    MoveEnemies(zone,Time.fixedDeltaTime);
                }
            }
        }

        private bool CheckIfZoneIsTriggered(ZoneView zone)
        {
            return zone.IsZoneTriggered;
        }

        private void MoveEnemies(ZoneView zone, float deltaTime) 
        {
            foreach(var enemy in zone.EnemiesPool) 
            {
                if (enemy.gameObject.activeInHierarchy) 
                {
                    RotateToPointIfNecessary(enemy, deltaTime);

                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.CurrentPatrolPoint, deltaTime * enemy.MovementSpeed);
                }
            }
        }

        private void RotateToPointIfNecessary(EnemyView enemy, float deltaTime) 
        {
            Quaternion rotationToPoint = GetRotation(enemy);
            if (!CheckRotation(enemy, rotationToPoint))
            {
                Rotate(enemy, deltaTime);
            }
        }

        private bool CheckRotation(EnemyView enemy, Quaternion rotaion) 
        {
            Vector3 direction = enemy.CurrentPatrolPoint - enemy.transform.position;
            float currentRotation = Vector3.Angle(enemy.transform.forward, direction);
            return currentRotation <= Inaccuracy;
        }

        private Quaternion GetRotation(EnemyView enemy) 
        {
            Vector3 direction = enemy.CurrentPatrolPoint - enemy.transform.position;
            return  Quaternion.LookRotation(direction);
        }

        private void Rotate(EnemyView enemy, float deltaTime) 
        {
            Vector3 direction = (enemy.CurrentPatrolPoint - enemy.transform.position) * deltaTime * enemy.RotationSpeed;
            enemy.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
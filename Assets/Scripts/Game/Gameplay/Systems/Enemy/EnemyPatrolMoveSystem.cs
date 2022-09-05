using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Models.Zone;

namespace Game.Gameplay.Systems.Enemy 
{  
    public class EnemyPatrolMoveSystem : IFixedUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;

        public const float Inaccuracy = 0.1f;

        public EnemyPatrolMoveSystem(ZonesInfo zonesInfo)
        {
            _zonesInfo = zonesInfo;
        }

        public void FixedUpdate()
        {
            foreach (var zone in _zonesInfo.Zones)
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
                    MakeMovement(enemy, deltaTime);
                }
            }
        }

        private void MakeMovement(EnemyView enemy, float deltaTime) 
        {
            Quaternion rotationToPoint = GetRotation(enemy);
            if (!CheckRotation(enemy, rotationToPoint))           
                Rotate(enemy, deltaTime);           
            else            
                Move(enemy, deltaTime);            
        }

        private void Move(EnemyView enemy, float deltaTime) 
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.CurrentPatrolPoint, deltaTime * enemy.MovementSpeed);
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
            if (direction != Vector3.zero)
                return Quaternion.LookRotation(direction);
            
             return   Quaternion.identity;
        }

        private void Rotate(EnemyView enemy, float deltaTime) 
        {
            Vector3 direction = enemy.CurrentPatrolPoint - enemy.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, deltaTime * enemy.RotationSpeed);
        }
    }
}
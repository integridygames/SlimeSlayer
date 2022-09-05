using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy 
{
    public class EnemyPersuePlayerSystem : IFixedUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;
        private readonly CharacterView _characterView;

        public const float Inaccuracy = 0.1f;

        public EnemyPersuePlayerSystem(ZonesInfo zonesInfo, CharacterView characterView)
        {
            _zonesInfo = zonesInfo;
            _characterView = characterView;
        }

        public void FixedUpdate()
        {   
            if (CheckIfCurrentZoneIsTriggered())
            {
                PursusePlayer(Time.fixedDeltaTime);
            }        
        }

        private bool CheckIfCurrentZoneIsTriggered()
        {
            return _zonesInfo.CurrentZone != null && _zonesInfo.CurrentZone.IsZoneTriggered;
        }

        private void PursusePlayer(float deltaTime) 
        {
            foreach(EnemyView enemy in _zonesInfo.CurrentZone.EnemiesPool) 
            {
                if (enemy.gameObject.activeInHierarchy) 
                {
                    RotateToPlayerIfNecessary(enemy, deltaTime);
                    MoveToPlayer(enemy, deltaTime);
                }          
            }
        }

        private void MoveToPlayer(EnemyView enemy, float deltaTime) 
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, new Vector3(_characterView.transform.position.x, enemy.transform.position.y, _characterView.transform.position.z), deltaTime * enemy.MovementSpeed);
        }

        private void RotateToPlayerIfNecessary(EnemyView enemy, float deltaTime)
        {
            Quaternion rotationToPoint = GetRotation(enemy);
            if (!CheckRotation(enemy, rotationToPoint))
            {
                Rotate(enemy, deltaTime);
            }
        }

        private bool CheckRotation(EnemyView enemy, Quaternion rotaion)
        {
            Vector3 direction = _characterView.transform.position - enemy.transform.position;
            float currentRotation = Vector3.Angle(enemy.transform.forward, direction);
            return currentRotation <= Inaccuracy;
        }

        private Quaternion GetRotation(EnemyView enemy)
        {
            Vector3 direction = _characterView.transform.position - enemy.transform.position;
            return Quaternion.LookRotation(direction);
        }

        private void Rotate(EnemyView enemy, float deltaTime)
        {
            Vector3 direction = _characterView.transform.position - enemy.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, deltaTime * enemy.RotationSpeed);
        }
    }
}
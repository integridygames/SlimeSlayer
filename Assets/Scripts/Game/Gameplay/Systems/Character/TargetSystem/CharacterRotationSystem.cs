using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class CharacterRotationSystem : IFixedUpdateSystem
    {
        private readonly CharacterMovementData _characterMovementData;
        private readonly CharacterView _characterView;
        private readonly CharacterStats _characterStats;

        public CharacterRotationSystem(CharacterMovementData characterMovementData, CharacterView characterView,
            CharacterStats characterStats)
        {
            _characterMovementData = characterMovementData;
            _characterView = characterView;
            _characterStats = characterStats;
        }

        public void FixedUpdate()
        {
            var direction = new Vector3(_characterMovementData.Direction.x, _characterView.transform.forward.y,
                _characterMovementData.Direction.z);

            if (direction != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(direction);

                _characterView.Rigidbody.rotation = Quaternion.RotateTowards(_characterView.transform.rotation,
                    targetRotation, Time.fixedDeltaTime * _characterStats.RotationSpeed);
            }
        }
    }
}
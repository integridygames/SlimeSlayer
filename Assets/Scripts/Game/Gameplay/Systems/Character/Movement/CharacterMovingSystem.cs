using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterMovingSystem : IFixedUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly CharacterMovementData _characterMovementData;

        public CharacterMovingSystem(CharacterView characterView, CharacterMovementData characterMovementData)
        {
            _characterView = characterView;
            _characterMovementData = characterMovementData;
        }
        
        public void FixedUpdate()
        {
            _characterView.Rigidbody.MovePosition(_characterView.Rigidbody.position + _characterMovementData.Velocity * Time.fixedDeltaTime);
        }
    }
}
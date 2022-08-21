using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterMovingSystem : IFixedUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly CharacterMovingData _characterMovingData;

        public CharacterMovingSystem(CharacterView characterView, CharacterMovingData characterMovingData)
        {
            _characterView = characterView;
            _characterMovingData = characterMovingData;
        }
        
        public void FixedUpdate()
        {
            _characterView.Rigidbody.MovePosition(_characterView.Rigidbody.position + _characterMovingData.Velocity * Time.fixedDeltaTime);
        }
    }
}
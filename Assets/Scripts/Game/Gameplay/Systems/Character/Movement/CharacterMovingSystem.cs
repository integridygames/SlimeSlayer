using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

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
            _characterView.Rigidbody.MovePosition(_characterMovementData.NextPosition);
        }
    }
}
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using TegridyUtils.UI.Joystick.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterInputVelocitySystem : IUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly CharacterMovementData _characterMovementData;
        private readonly CharacterView _characterView;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterInputVelocitySystem(Joystick joystick, CharacterMovementData characterMovementData,
            CharacterView characterView, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _joystick = joystick;
            _characterMovementData = characterMovementData;
            _characterView = characterView;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public void Update()
        {
            var movingVector = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            if (movingVector.sqrMagnitude <= 0.1f)
            {
                movingVector = Vector3.zero;
            }

            _characterMovementData.MovingVector = movingVector;
            _characterMovementData. NextPosition = _characterView.Rigidbody.position +
                                                  movingVector * _characterCharacteristicsRepository.MovingSpeed *
                                                  Time.deltaTime;
        }
    }
}
using Game.Gameplay.Models.Character;
using TegridyCore.Base;
using TegridyUtils.UI.Joystick.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterInputVelocitySystem : IUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly CharacterMovementData _characterMovementData;
        private readonly CharacterStats _characterStats;

        public CharacterInputVelocitySystem(Joystick joystick, CharacterMovementData characterMovementData,
            CharacterStats characterStats)
        {
            _joystick = joystick;
            _characterMovementData = characterMovementData;
            _characterStats = characterStats;
        }

        public void Update()
        {
            var movingVector = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

            _characterMovementData.Velocity = movingVector * _characterStats.MovingSpeed;
        }
    }
}
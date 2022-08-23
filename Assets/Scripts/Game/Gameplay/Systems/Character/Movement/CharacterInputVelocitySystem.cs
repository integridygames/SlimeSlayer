using Game.Gameplay.Models.Character;
using TegridyCore.Base;
using TegridyUtils.UI.Joystick.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterInputVelocitySystem : IUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly CharacterMovingData _characterMovingData;
        private readonly CharacterStats _characterStats;

        public CharacterInputVelocitySystem(Joystick joystick, CharacterMovingData characterMovingData,
            CharacterStats characterStats)
        {
            _joystick = joystick;
            _characterMovingData = characterMovingData;
            _characterStats = characterStats;
        }

        public void Update()
        {
            var movingVector = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

            _characterMovingData.Velocity = movingVector * _characterStats.MovingSpeed;
        }
    }
}
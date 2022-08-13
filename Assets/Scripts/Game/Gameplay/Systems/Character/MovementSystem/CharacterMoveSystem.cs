using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Input;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Systems.Character.MovementSystem 
{
    public class CharacterMoveSystem : IFixedUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly Rigidbody _characterRigidBody;

        private Vector3 _currentJoystickPosition;
        private Vector3 _previousJoystickPosition;
        private Vector3 _movementDirection;

        private const float Inaccuracy = 0.05f;
        private const float Velocity = 0.7f;
        private const float Speed = 1000f;

        public CharacterMoveSystem(Joystick joystick, CharacterView characterView) 
        {
            _joystick = joystick;
            _currentJoystickPosition = Vector3.zero;
            _previousJoystickPosition = Vector3.zero;
            characterView.TryGetComponent<Rigidbody>(out _characterRigidBody);
        }

        public void FixedUpdate()
        {
            if (_joystick.gameObject.activeInHierarchy) 
            {
                _currentJoystickPosition = _joystick.Handle.position;
                TryToMove();
            }
        }     

        private void TryToMove() 
        {
            if(_currentJoystickPosition.x != _previousJoystickPosition.x || _currentJoystickPosition.y != _previousJoystickPosition.y) 
            {                
                _movementDirection = DetermineDirectionOfEachCoordinate();
                _previousJoystickPosition = _currentJoystickPosition;
                Move();
            }
        }

        private void Move() 
        {
            if(_characterRigidBody.velocity != null) 
            {
                _characterRigidBody.velocity = Vector3.MoveTowards(_characterRigidBody.velocity, _movementDirection * Velocity, Time.fixedDeltaTime * Speed);               
            }
        }

        private Vector3 DetermineDirectionOfEachCoordinate() 
        {       
            float directionX = DetermineDirection(_currentJoystickPosition.x, _previousJoystickPosition.x);
            float directionZ = DetermineDirection(_currentJoystickPosition.y, _previousJoystickPosition.y);

            return new Vector3(-directionX, 0, -directionZ);
        }

        private float DetermineDirection(float currentPosition, float previousPosition) 
        {
            float direction = currentPosition - previousPosition;
            if (Mathf.Abs(direction) >= Inaccuracy)
            {
                return direction / Mathf.Abs(direction);
            }
            else
                return 0;
        }
    }
}
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

        private Vector3 _movementDirection;

        private const float Inaccuracy = 30f;
        private const float Speed = 1f;

        public CharacterMoveSystem(Joystick joystick, CharacterView characterView) 
        {
            _joystick = joystick;
            characterView.TryGetComponent<Rigidbody>(out _characterRigidBody);
        }

        public void FixedUpdate()
        {
            if (_joystick.gameObject.activeInHierarchy) 
            {
                TryToMove();
            }
        }     

        private void TryToMove() 
        {
            _movementDirection = DetermineDirectionOfEachCoordinate(); 
            Move();    
        }

        private void Move() 
        {
            _characterRigidBody.MovePosition(_characterRigidBody.transform.position + _movementDirection * Time.fixedDeltaTime * Speed);
        }

        private Vector3 DetermineDirectionOfEachCoordinate() 
        {
            float directionX, directionZ;
          
            directionX = DetermineDirection(_joystick.Handle.transform.position.x, _joystick.Background.transform.position.x);
            directionZ = DetermineDirection(_joystick.Handle.transform.position.y, _joystick.Background.transform.position.y);
            
            return new Vector3(-directionX, 0, -directionZ);
        }

        private float DetermineDirection(float handlePosition, float centerPosition) 
        {
            float direction = handlePosition - centerPosition;
            if (Mathf.Abs(direction) >= Inaccuracy)
            {
                return direction / Mathf.Abs(direction);
            }
            else
                return 0;
        }
    }
}
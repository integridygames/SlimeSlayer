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

        private const float Speed = 2f;

        public CharacterMoveSystem(Joystick joystick, CharacterView characterView) 
        {
            _joystick = joystick;
            characterView.TryGetComponent<Rigidbody>(out _characterRigidBody);
        }

        public void FixedUpdate()
        {
            if (_joystick.gameObject.activeInHierarchy) 
            {
                Move();
            }
        }     
     
        private void Move() 
        {
            _characterRigidBody.MovePosition(_characterRigidBody.transform.position + _joystick.ConvertHandleLocalToWorldPosition() * Time.fixedDeltaTime * Speed);
        }
    }
}
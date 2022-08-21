using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Input;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Utils.Character.Animator;

namespace Game.Gameplay.Systems.Character.MovementSystem
{
    public class CharacterMoveAnimatorSystem : IUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly CharacterView _characterView;
        private readonly Animator _characterAnimator;

        private Vector3 _currentCharacterPosition;
        private Vector3 _previousCharacterPosition;

        public CharacterMoveAnimatorSystem(Joystick joystick, CharacterView characterView) 
        {
            _joystick = joystick;
            _characterView = characterView;
            _characterAnimator = _characterView.GetComponentInChildren<Animator>();
            _currentCharacterPosition = Vector3.zero;
            _previousCharacterPosition = Vector3.zero;
        }

        public void Update()
        {
            if (CheckIfCharacterInMove())
            {
                SetMovementAnimations();
            }          
        }

        private void SetSpeed(float speed) 
        {
            _characterAnimator.SetFloat(nameof(AnimatorParams.Speed), speed);
        }

        private void SetMovementAnimations() 
        {         
            CalculatePositionsInLocalCoordinates(_joystick.HandleNormalizedLocalPosition, out float localPoitionX, out float localPositionY);

            _characterAnimator.SetFloat(nameof(AnimatorParams.PosX), localPoitionX);
            _characterAnimator.SetFloat(nameof(AnimatorParams.PosY), localPositionY);
        }      

        private void CalculatePositionsInLocalCoordinates(Vector3 handleNormalizedLocalPosition, out float localPositionX, out float localPositionY) 
        {
            float angleSinus = Mathf.Sin(_characterView.transform.localEulerAngles.y * Mathf.Deg2Rad);
            float angleCosinus = Mathf.Cos(_characterView.transform.localEulerAngles.y * Mathf.Deg2Rad);
            localPositionX = handleNormalizedLocalPosition.x * angleCosinus - handleNormalizedLocalPosition.y * angleSinus;
            localPositionY = handleNormalizedLocalPosition.x * angleSinus + handleNormalizedLocalPosition.y * angleCosinus;
        }

        private bool CheckIfCharacterInMove() 
        {
            _currentCharacterPosition = _characterView.transform.position;

            if (_currentCharacterPosition != _previousCharacterPosition)
            {
                _previousCharacterPosition = _currentCharacterPosition;
                return true;
            }
            else
                return false;       
        }
    }  
}
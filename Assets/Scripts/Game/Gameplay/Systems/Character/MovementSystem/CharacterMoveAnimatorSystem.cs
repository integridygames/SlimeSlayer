using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Input;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Utils.Character.Animator;
using System;

namespace Game.Gameplay.Systems.Character.MovementSystem
{
    public class CharacterMoveAnimatorSystem : IUpdateSystem
    {
        private readonly Joystick _joystick;
        private readonly CharacterView _characterView;
        private readonly Animator _characterAnimator;

        private Vector3 _currentPosition;
        private Vector3 _currentCharacterPosition;
        private Vector3 _previousCharacterPosition;

        private const float MaxDifferenceX = 118;
        private const float MaxDifferenceY = 118;
        private const float Speed = 1;

        public CharacterMoveAnimatorSystem(Joystick joystick, CharacterView characterView) 
        {
            _joystick = joystick;
            _characterView = characterView;
            _characterAnimator = _characterView.GetComponentInChildren<Animator>();
            _currentPosition = Vector3.zero;
            _currentCharacterPosition = Vector3.zero;
            _previousCharacterPosition = Vector3.zero;
        }

        public void Update()
        {
            if (CheckIfCharacterInMove())
            {
                SetSpeed(Speed);
                SetMovementAnimations();
            }
            else
                SetSpeed(0);
        }

        private void SetSpeed(float speed) 
        {
            _characterAnimator.SetFloat(nameof(AnimatorParams.Speed), speed);
        }

        private void SetMovementAnimations() 
        {
            _currentPosition = _joystick.Handle.transform.position - _joystick.Background.transform.position;
            float positionX = _currentPosition.x / MaxDifferenceX;
            float positionY = _currentPosition.y / MaxDifferenceY;

            CalculatePositionsInLocalCoordinates(positionX, positionY, out float localPoitionX, out float localPositionY);

            _characterAnimator.SetFloat(nameof(AnimatorParams.PosX), localPoitionX);
            _characterAnimator.SetFloat(nameof(AnimatorParams.PosY), localPositionY);
        }      

        private void CalculatePositionsInLocalCoordinates(float positionX, float positionY, out float localPoitionX, out float localPositionY) 
        {
            float angleSinus = Mathf.Sin(_characterView.transform.localEulerAngles.y * Mathf.Deg2Rad);
            float angleCosinus = Mathf.Cos(_characterView.transform.localEulerAngles.y * Mathf.Deg2Rad);
            localPoitionX = positionX * angleCosinus - positionY * angleSinus;
            localPositionY = positionX * angleSinus + positionY * angleCosinus;
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
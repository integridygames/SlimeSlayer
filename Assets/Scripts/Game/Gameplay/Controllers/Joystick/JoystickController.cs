using Game.Gameplay.Utils.Character.Animator;
using Game.Gameplay.Views.Character;
using System;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Joystick 
{
    public class JoystickController : ControllerBase<Views.Input.Joystick>, IInitializable, IDisposable
    {
        private readonly Views.Input.Joystick _joystick;
        private readonly Animator _characterAnimator;

        public JoystickController(Views.Input.Joystick joystick, CharacterView characterView) : base(joystick)
        {
            _joystick = joystick;
            _characterAnimator = characterView.GetComponentInChildren<Animator>();

        }

        public void Initialize()
        {
            ControlledEntity.OnHide += SetDefaultHandlePosition;
        }

        public void Dispose()
        {
            ControlledEntity.OnHide -= SetDefaultHandlePosition;
        }

        private void SetDefaultHandlePosition() 
        {
            _joystick.CalculateHandleLocalPosition();
            SetCharacterAnimationToIdle();
        }

        private void SetCharacterAnimationToIdle() 
        {
            _characterAnimator.SetFloat(nameof(AnimatorParams.PosX), 0);
            _characterAnimator.SetFloat(nameof(AnimatorParams.PosY), 0);
        }
    }   
}

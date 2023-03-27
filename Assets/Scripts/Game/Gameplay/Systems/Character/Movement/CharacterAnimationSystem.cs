using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterAnimationSystem : IUpdateSystem
    {
        private const float LerpSpeed = 20;

        private static readonly int PosY = Animator.StringToHash("PosY");
        private static readonly int PosX = Animator.StringToHash("PosX");

        private readonly CharacterView _characterView;
        private readonly CharacterMovementData _characterMovementData;

        private Vector3 _currentAnimationVelocity;

        public CharacterAnimationSystem(CharacterView characterView, CharacterMovementData characterMovementData)
        {
            _characterView = characterView;
            _characterMovementData = characterMovementData;
        }

        public void Update()
        {
            var inversedToLocalVelocity =
                _characterView.transform.InverseTransformDirection(_characterMovementData.MovingVector);

            _currentAnimationVelocity = Vector3.ClampMagnitude(Vector3.Lerp(_currentAnimationVelocity,
                inversedToLocalVelocity,
                Time.deltaTime * LerpSpeed), 1);

            _characterView.Animator.SetFloat(PosX, _currentAnimationVelocity.x);
            _characterView.Animator.SetFloat(PosY, _currentAnimationVelocity.z);
        }
    }
}
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterAnimationSystem : IUpdateSystem
    {
        private const float LerpSpeed = 10;
        
        private static readonly int PosY = Animator.StringToHash("PosY");
        private static readonly int PosX = Animator.StringToHash("PosX");
        
        private readonly CharacterView _characterView;
        private readonly CharacterMovingData _characterMovingData;

        private Vector3 _currentAnimationVelocity;

        public CharacterAnimationSystem(CharacterView characterView, CharacterMovingData characterMovingData)
        {
            _characterView = characterView;
            _characterMovingData = characterMovingData;
        }
        
        public void Update()
        {
            var normalizedVelocity = _characterMovingData.Velocity.normalized;

            var inversedToLocalVelocity = _characterView.transform.InverseTransformDirection(normalizedVelocity);

            _currentAnimationVelocity = Vector3.Lerp(_currentAnimationVelocity, inversedToLocalVelocity,
                Time.deltaTime * LerpSpeed);
            
            _characterView.Animator.SetFloat(PosX, _currentAnimationVelocity.x);
            _characterView.Animator.SetFloat(PosY, _currentAnimationVelocity.z);
        }
    }
}
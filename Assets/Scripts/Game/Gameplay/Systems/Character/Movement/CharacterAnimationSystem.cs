using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Heap;
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
        private readonly CharacterMovementData _characterMovementData;
        private readonly HeapInfo _heapInfo;

        private Vector3 _currentAnimationVelocity;

        public CharacterAnimationSystem(CharacterView characterView, CharacterMovementData characterMovementData, HeapInfo heapInfo)
        {
            _characterView = characterView;
            _characterMovementData = characterMovementData;
            _heapInfo = heapInfo;
        }
        
        public void Update()
        {
            /*_characterView.Animator.SetLayerWeight(1, _heapInfo.FoundHeap ? 1 : 0);*/

            var normalizedVelocity = _characterMovementData.Velocity.normalized;

            var inversedToLocalVelocity = _characterView.transform.InverseTransformDirection(normalizedVelocity);

            _currentAnimationVelocity = Vector3.Lerp(_currentAnimationVelocity, inversedToLocalVelocity,
                Time.deltaTime * LerpSpeed);
            
            _characterView.Animator.SetFloat(PosX, _currentAnimationVelocity.x);
            _characterView.Animator.SetFloat(PosY, _currentAnimationVelocity.z);
        }
    }
}
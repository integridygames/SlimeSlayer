using Game.Gameplay.Models.Heap;
using TegridyCore.Base;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Models.Character.TargetSystem;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem 
{
    public class CharacterRotatorToNearestHeapSystem : IFixedUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly HeapInfo _heapInfo;
        private readonly float _rotationSpeed;

        public CharacterRotatorToNearestHeapSystem(CharacterView characterView, HeapInfo heapInfo, CharacterHandsMovingStats statsInfo)
        {
            _heapInfo = heapInfo;
            _characterView = characterView;
            _rotationSpeed = statsInfo.RotationSpeed;
        }

        public void FixedUpdate()
        {
            if (_heapInfo.HeapVector != Vector3.zero)
            {
                Vector3 direction = _heapInfo.HeapVector - _characterView.transform.position;
                direction = new Vector3(direction.x, _characterView.transform.position.y, direction.z);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                _characterView.Rigidbody.rotation = Quaternion.RotateTowards(_characterView.transform.rotation, 
                    targetRotation, Time.fixedDeltaTime * _rotationSpeed);
            }
        }
    }
}
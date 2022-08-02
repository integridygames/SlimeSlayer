using Game.Gameplay.Models.Heap;
using TegridyCore.Base;
using Game.Gameplay.Views.Character;
using UnityEngine;

namespace Game.Gameplay.Systems.Character 
{
    public class CharacterToNearestHeapMoverSystem : IFixedUpdateSystem
    {
        private CharacterView _characterView;
        private HeapInfo _heapInfo;

        private const float _rotationSpeed = 100;

        public CharacterToNearestHeapMoverSystem(CharacterView characterView, HeapInfo heapInfo)
        {
            _heapInfo = heapInfo;
            _characterView = characterView;
        }

        public void FixedUpdate()
        {
            if (_heapInfo.HeapVector != Vector3.zero)
            {
                Vector3 direction = _heapInfo.HeapVector - _characterView.transform.position;
                direction = new Vector3(direction.x, _characterView.transform.position.y, direction.z);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _characterView.transform.rotation = Quaternion.RotateTowards(_characterView.transform.rotation, 
                    targetRotation, Time.fixedDeltaTime * _rotationSpeed);
            }
        }
    }
}
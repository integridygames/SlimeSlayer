using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Heap;
using TegridyCore.Base;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class CharacterDirectionFinderSystem : IUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly HeapInfo _heapInfo;
        private readonly CharacterMovementData _characterMovementData;

        public CharacterDirectionFinderSystem(CharacterView characterView, HeapInfo heapInfo,
            CharacterMovementData characterMovementData)
        {
            _heapInfo = heapInfo;
            _characterMovementData = characterMovementData;
            _characterView = characterView;
        }

        public void Update()
        {
            if (_heapInfo.FoundHeap)
            {
                var distanceVector = _heapInfo.ClosestHeapPosition - _characterView.transform.position;

                _characterMovementData.Direction = distanceVector.normalized;
                return;
            }

            if (_characterMovementData.Velocity.magnitude > 0)
            {
                _characterMovementData.Direction = _characterMovementData.Velocity;
            }
        }
    }
}
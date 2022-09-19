using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using System;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTransitView : ViewBase
    {
        public event Action OnCharacterEntered;
        public event Action OnCharacterLeft;

        [SerializeField] private ZoneView _zoneToTransit;
        [SerializeField] private bool _isOpened;
        [SerializeField] private ZoneTransitEssenceData[] _essenceData;       

        public bool IsOpened => _isOpened;
        public ZoneTransitEssenceData[] EssenceData => _essenceData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView characterView) && !_isOpened)
                OnCharacterEntered?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterView characterView) && !_isOpened)
                OnCharacterLeft?.Invoke();
        }    

        public void Open() 
        {
            _isOpened = true;
        }

        public void Close() 
        {
            _isOpened = false;
        }
    }
}
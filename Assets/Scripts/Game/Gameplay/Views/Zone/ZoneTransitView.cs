using Game.Gameplay.Views.Character;
using System;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTransitView : ViewBase
    {
        public event Action OnCharacterEntered;

        [SerializeField] private ZoneView _zoneToTransit;
        [SerializeField] private bool _isOpened;

        public bool IsOpened => _isOpened;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView characterView) && !_isOpened)
                OnCharacterEntered?.Invoke();
        }
    }
}
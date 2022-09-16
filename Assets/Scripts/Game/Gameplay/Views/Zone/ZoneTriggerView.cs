using Game.Gameplay.Views.Character;
using System;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class ZoneTriggerView : ViewBase
    {
        public event Action OnCharacterExit;

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CharacterView>(out CharacterView characterView))
            {
                OnCharacterExit?.Invoke();
            }
        }
    }
}
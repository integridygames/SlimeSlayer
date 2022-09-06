using System;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Level
{
    public class FinishView : ViewBase
    {
        public event Action OnPlayerEntered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView _))
            {
                OnPlayerEntered?.Invoke();
            }
        }
    }
}
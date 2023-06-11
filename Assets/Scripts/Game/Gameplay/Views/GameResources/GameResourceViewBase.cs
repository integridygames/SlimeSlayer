using System;
using Game.DataBase.Essence;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.GameResources
{
    public abstract class GameResourceViewBase : ViewBase
    {
        public event Action<GameResourceViewBase> OnResourceCollide;
        public abstract GameResourceType GameResourceType { get; }
        public bool IsMovingToCharacter { get; set; }
        public float MovingProgress { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView _))
            {
                OnResourceCollide?.Invoke(this);
            }

            if (other.CompareTag("EssenceTrigger"))
            {
                IsMovingToCharacter = true;
            }
        }
    }
}
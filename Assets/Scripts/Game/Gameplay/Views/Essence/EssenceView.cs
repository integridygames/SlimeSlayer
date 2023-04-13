using Game.Gameplay.Views.Character;
using System;
using Game.DataBase.Essence;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Essence 
{
    public class EssenceView : ViewBase
    {
        public event Action<EssenceView> OnEssenceCollide;

        [SerializeField] private EssenceType _essenceType;

        public int Quantity => 1;
        public EssenceType EssenceType => _essenceType;

        public bool IsMovingToCharacter { get; set; }

        public float MovingProgress { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView _))
            {
                OnEssenceCollide?.Invoke(this);
            }

            if (other.CompareTag("EssenceTrigger"))
            {
                IsMovingToCharacter = true;
            }
        }
    }
}
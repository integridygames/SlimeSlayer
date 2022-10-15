using Game.Gameplay.Views.Character;
using System;
using Game.DataBase;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Essence 
{
    public class EssenceView : ViewBase
    {
        public event Action<EssenceView> OnEssenceCollide;

        [SerializeField] private EssenceType _essenceType;

        public int Quantity { get; private set; }
        public EssenceType EssenceType => _essenceType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterView characterView))
            {
                OnEssenceCollide?.Invoke(this);
            }
        }

        public void Recycle()
        {
            gameObject.SetActive(false);
        }

        public void SetQuantity(int quantity) 
        {
            Quantity = quantity;
        }
    }
}
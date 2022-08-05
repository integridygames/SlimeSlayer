using UnityEngine;
using TegridyCore.Base;

namespace Game.Gameplay.Views.Bullets 
{
    public class AmmoView : ViewBase
    {
        [SerializeField] private string _identificator;
        [SerializeField] private int _maxQuantity;
        [SerializeField] private bool _isUnlimited;

        private int _quantity;
        public int MaxQuantity => _maxQuantity;
        public int Quantity => _quantity;
        public string ID => _identificator;
        public bool IsUnlimited => _isUnlimited;

        public void AddAmmo(int quantity)
        {
            _quantity += quantity;
        }

        public void RemoveAmmo(int quantity)
        {
            int current = _quantity - quantity;
            _quantity = Mathf.Clamp(current, 0, _maxQuantity);
            _quantity -= quantity;
        }
    }
}
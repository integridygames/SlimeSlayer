using TegridyCore.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Character
{
    public class CharacterHealthView : ViewBase
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private float _verticalOffset;

        public float VerticalOffset => _verticalOffset;

        public void SetHealthPercentage(float health)
        {
            _healthBar.fillAmount = health;
        }
    }
}
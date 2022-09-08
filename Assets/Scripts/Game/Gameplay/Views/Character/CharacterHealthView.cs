using TegridyCore.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Character
{
    public class CharacterHealthView : ViewBase
    {
        [SerializeField] private Image _healthBar;
        
        public void SetHealthPercentage(float health)
        {
            _healthBar.fillAmount = health;
        }
    }
}
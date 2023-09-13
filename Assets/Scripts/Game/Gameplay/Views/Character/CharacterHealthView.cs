using Game.Gameplay.Views.Weapon;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character
{
    public class CharacterHealthView : ViewBase
    {
        [SerializeField] private ProgressBarView _healthBar;
        [SerializeField] private float _verticalOffset;

        public float VerticalOffset => _verticalOffset;

        public void SetHealthPercentage(float health)
        {
            _healthBar.SetProgress(health);
        }
    }
}
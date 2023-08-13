using TegridyCore.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI.Screens.Gameplay.Abilities
{
    public class SettingsAbilityView : ViewBase
    {
        [SerializeField] private Image _abilityImage;
        [SerializeField] private TMP_Text _abilityLevel;

        private string _abilityName;
        private string _description;

        public void SetData(Sprite sprite, int level, string abilityName, string description)
        {
            _abilityImage.sprite = sprite;
            _abilityLevel.text = $"Lv.{level}";
            _description = description;
            _abilityName = abilityName;
        }
    }
}
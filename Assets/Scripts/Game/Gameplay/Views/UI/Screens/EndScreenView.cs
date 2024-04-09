using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class EndScreenView : ViewBase
    {
        [SerializeField] private UiButton _completeButton;
        [SerializeField] private WeaponCardsView _weaponCardsView;
        [SerializeField] private TMP_Text _goldEarnedValue;

        public UiButton CompleteButton => _completeButton;

        public WeaponCardsView WeaponCardsView => _weaponCardsView;

        public TMP_Text GoldEarnedValue => _goldEarnedValue;
    }
}
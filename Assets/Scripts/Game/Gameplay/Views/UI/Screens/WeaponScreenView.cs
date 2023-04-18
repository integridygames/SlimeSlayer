using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class WeaponScreenView : ViewBase
    {
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private Transform _leftWeaponCardRoot;
        [SerializeField] private Transform _rightWeaponCardRoot;

        public UiButton CloseButton => _closeButton;

        public Transform LeftWeaponCardRoot => _leftWeaponCardRoot;

        public Transform RightWeaponCardRoot => _rightWeaponCardRoot;
    }
}
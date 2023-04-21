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
        [SerializeField] private UiButton _leftWeaponButton;
        [SerializeField] private UiButton _rightWeaponButton;
        [SerializeField] private GameObject _weaponsCardsContainer;
        [SerializeField] private Transform _weaponsCardsRoot;
        [SerializeField] private WeaponInfoView _weaponInfoView;

        public UiButton CloseButton => _closeButton;

        public Transform LeftWeaponCardRoot => _leftWeaponCardRoot;

        public Transform RightWeaponCardRoot => _rightWeaponCardRoot;

        public UiButton LeftWeaponButton => _leftWeaponButton;

        public UiButton RightWeaponButton => _rightWeaponButton;

        public GameObject WeaponsCardsContainer => _weaponsCardsContainer;

        public Transform WeaponsCardsRoot => _weaponsCardsRoot;

        public WeaponInfoView WeaponInfoView => _weaponInfoView;
    }
}
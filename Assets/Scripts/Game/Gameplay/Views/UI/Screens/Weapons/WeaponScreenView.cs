using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Weapons
{
    public class WeaponScreenView : ViewBase
    {
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private UiButton _toLeftWeaponButton;
        [SerializeField] private UiButton _toRightWeaponButton;
        [SerializeField] private GameObject _weaponsCardsContainer;
        [SerializeField] private WeaponInfoView _weaponInfoView;
        [SerializeField] private WeaponCardsView _weaponWeaponCardsView;
        [SerializeField] private GameObject _leftWeaponState;
        [SerializeField] private GameObject _rightWeaponState;

        public UiButton CloseButton => _closeButton;

        public GameObject WeaponsCardsContainer => _weaponsCardsContainer;

        public WeaponInfoView WeaponInfoView => _weaponInfoView;

        public WeaponCardsView WeaponCardsView => _weaponWeaponCardsView;

        public UiButton ToLeftWeaponButton => _toLeftWeaponButton;

        public UiButton ToRightWeaponButton => _toRightWeaponButton;

        public GameObject LeftWeaponState => _leftWeaponState;

        public GameObject RightWeaponState => _rightWeaponState;
    }
}
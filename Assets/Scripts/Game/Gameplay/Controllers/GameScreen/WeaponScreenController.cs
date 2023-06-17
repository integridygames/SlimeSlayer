using System;
using System.Linq;
using Game.DataBase.Essence;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.UI;
using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.Base;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class WeaponScreenController : ControllerBase<WeaponScreenView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly WeaponsDataBase _weaponsDataBase;
        private readonly CharacterView _characterView;
        private readonly WeaponFactory _weaponFactory;
        private readonly GameResourceData _gameResourceData;
        private readonly WeaponsService _weaponsService;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        private WeaponCardView _leftWeaponCardView;
        private WeaponCardView _rightWeaponCardView;

        private bool _isLeftWeaponPressed;

        public WeaponScreenController(WeaponScreenView controlledEntity, ApplicationData applicationData,
            CurrentCharacterWeaponsData currentCharacterWeaponsData, WeaponsDataBase weaponsDataBase,
            CharacterView characterView, WeaponFactory weaponFactory, GameResourceData gameResourceData,
            WeaponsService weaponsService, WeaponsCharacteristics weaponsCharacteristics) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _weaponsDataBase = weaponsDataBase;
            _characterView = characterView;
            _weaponFactory = weaponFactory;
            _gameResourceData = gameResourceData;
            _weaponsService = weaponsService;
            _weaponsCharacteristics = weaponsCharacteristics;
        }

        public void Initialize()
        {
            SpawnEquippedWeaponCards();

            ControlledEntity.CloseButton.OnReleased += OnCloseButtonPressedHandler;
            ControlledEntity.LeftWeaponButton.OnReleased += OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnReleased += OnRightWeaponButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnEquip += OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose += OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade += OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow += OnWeaponScreenShow;
            ControlledEntity.OnHide += OnWeaponScreenHide;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed += OnWeaponCardPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.CloseButton.OnReleased -= OnCloseButtonPressedHandler;
            ControlledEntity.LeftWeaponButton.OnReleased -= OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnReleased -= OnRightWeaponButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnEquip -= OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose -= OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade -= OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow -= OnWeaponScreenShow;
            ControlledEntity.OnHide -= OnWeaponScreenHide;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed -= OnWeaponCardPressedHandler;
        }

        private void OnLeftWeaponButtonPressedHandler()
        {
            _isLeftWeaponPressed = true;

            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(true);
        }

        private void OnRightWeaponButtonPressedHandler()
        {
            _isLeftWeaponPressed = false;

            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(true);
        }

        private void OnWeaponScreenShow()
        {
            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(false);
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);

            ControlledEntity.WeaponCardsView.ShowWeaponCards(_applicationData.PlayerData.WeaponsSaveData,
                _weaponsService);
        }

        private void SpawnEquippedWeaponCards()
        {
            var weaponSaveDataLeft =
                _applicationData.PlayerData.WeaponsSaveData.First(x =>
                    x._guid == _applicationData.PlayerData.CurrentLeftWeaponGuid);
            var weaponSaveDataRight =
                _applicationData.PlayerData.WeaponsSaveData.First(x =>
                    x._guid == _applicationData.PlayerData.CurrentRightWeaponGuid);

            _leftWeaponCardView = _weaponsService.SpawnWeaponCard(ControlledEntity.LeftWeaponCardRoot,
                weaponSaveDataLeft);
            _rightWeaponCardView = _weaponsService.SpawnWeaponCard(ControlledEntity.RightWeaponCardRoot,
                weaponSaveDataRight);
        }

        private void OnWeaponScreenHide()
        {
            ControlledEntity.WeaponCardsView.Clear();
        }

        private void OnCloseButtonPressedHandler()
        {
            if (ControlledEntity.WeaponsCardsContainer.activeSelf)
            {
                ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(false);
            }
        }

        private void OnWeaponCardPressedHandler(PlayerWeaponData playerWeaponData, WeaponCardView _)
        {
            SetDataToWeaponInfoView(playerWeaponData);

            ControlledEntity.WeaponInfoView.gameObject.SetActive(true);
        }

        private void OnUpgradeButtonPressedHandler(PlayerWeaponData playerWeaponData)
        {
            var price = (int) _weaponsCharacteristics.GetCharacteristic(
                playerWeaponData, WeaponCharacteristicType.UpgradePrice);

            _gameResourceData.AddResource(GameResourceType.Coin, -price);

            playerWeaponData._level++;

            _weaponsCharacteristics.UpdateCharacteristics(playerWeaponData);

            SetDataToWeaponInfoView(playerWeaponData);
        }

        private void SetDataToWeaponInfoView(PlayerWeaponData playerWeaponData)
        {
            var isEquipped = playerWeaponData == _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value.Data ||
                             playerWeaponData == _currentCharacterWeaponsData.CurrentWeaponViewRight.Value.Data;

            ControlledEntity.WeaponInfoView.SetWeapon(playerWeaponData, isEquipped,
                _weaponsCharacteristics, _weaponsDataBase,
                _applicationData.PlayerData.CurrentCoinsCount);
        }

        private void OnEquipButtonPressedHandler(PlayerWeaponData playerWeaponData)
        {
            if (_isLeftWeaponPressed)
            {
                _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value.Destroy();
                _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.LeftWeaponPlacer, true);

                Object.Destroy(_leftWeaponCardView.gameObject);
                _leftWeaponCardView = _weaponsService.SpawnWeaponCard(ControlledEntity.LeftWeaponCardRoot,
                    playerWeaponData);
            }
            else
            {
                _currentCharacterWeaponsData.CurrentWeaponViewRight.Value.Destroy();
                _currentCharacterWeaponsData.CurrentWeaponViewRight.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.RightWeaponPlacer, false);

                Object.Destroy(_rightWeaponCardView.gameObject);
                _rightWeaponCardView = _weaponsService.SpawnWeaponCard(ControlledEntity.RightWeaponCardRoot,
                    playerWeaponData);
            }

            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
            OnCloseButtonPressedHandler();
        }

        private void OnWeaponInfoCloseButtonPressedHandler()
        {
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
        }
    }
}
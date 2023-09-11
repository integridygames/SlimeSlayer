using System;
using System.Collections.Generic;
using Game.DataBase.GameResource;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.UI;
using Game.Gameplay.Views.UI.Screens.Weapons;
using Game.Services;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class WeaponScreenController : ControllerBase<WeaponScreenView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly CharacterWeaponsRepository _characterWeaponsRepository;
        private readonly WeaponsDataBase _weaponsDataBase;
        private readonly CharacterView _characterView;
        private readonly WeaponFactory _weaponFactory;
        private readonly GameResourceData _gameResourceData;
        private readonly WeaponsService _weaponsService;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly ResourceShortFormsDataBase _resourceShortFormsDataBase;

        private WeaponCardView _leftWeaponCardView;
        private WeaponCardView _rightWeaponCardView;

        private bool _isLeftWeaponState;

        public WeaponScreenController(WeaponScreenView controlledEntity, ApplicationData applicationData,
            CharacterWeaponsRepository characterWeaponsRepository, WeaponsDataBase weaponsDataBase,
            CharacterView characterView, WeaponFactory weaponFactory, GameResourceData gameResourceData,
            WeaponsService weaponsService, WeaponsCharacteristics weaponsCharacteristics,
            ResourceShortFormsDataBase resourceShortFormsDataBase) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _characterWeaponsRepository = characterWeaponsRepository;
            _weaponsDataBase = weaponsDataBase;
            _characterView = characterView;
            _weaponFactory = weaponFactory;
            _gameResourceData = gameResourceData;
            _weaponsService = weaponsService;
            _weaponsCharacteristics = weaponsCharacteristics;
            _resourceShortFormsDataBase = resourceShortFormsDataBase;
        }

        public void Initialize()
        {
            ControlledEntity.WeaponInfoView.OnEquip += OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose += OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade += OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow += OnWeaponScreenShow;
            ControlledEntity.OnHide += OnWeaponScreenHide;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed += OnWeaponCardPressedHandler;
            ControlledEntity.ToLeftWeaponButton.OnReleased += OnToLeftWeaponButtonPressedHandler;
            ControlledEntity.ToRightWeaponButton.OnReleased += OnToRightWeaponButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.WeaponInfoView.OnEquip -= OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose -= OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade -= OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow -= OnWeaponScreenShow;
            ControlledEntity.OnHide -= OnWeaponScreenHide;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed -= OnWeaponCardPressedHandler;
            ControlledEntity.ToLeftWeaponButton.OnReleased -= OnToLeftWeaponButtonPressedHandler;
            ControlledEntity.ToRightWeaponButton.OnReleased -= OnToRightWeaponButtonPressedHandler;
        }

        private void OnToLeftWeaponButtonPressedHandler()
        {
            SetLeftState();
        }

        private void SetLeftState()
        {
            ControlledEntity.LeftWeaponState.SetActive(true);
            ControlledEntity.RightWeaponState.SetActive(false);

            _isLeftWeaponState = true;
            ResetWeapons();
        }

        private void OnToRightWeaponButtonPressedHandler()
        {
            SetRightState();
            ResetWeapons();
        }

        private void SetRightState()
        {
            ControlledEntity.RightWeaponState.SetActive(true);
            ControlledEntity.LeftWeaponState.SetActive(false);

            _isLeftWeaponState = false;
        }

        private void OnWeaponScreenShow()
        {
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(true);

            SetLeftState();
        }

        private void OnWeaponScreenHide()
        {
            ControlledEntity.WeaponCardsView.Clear();
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
            ResetWeapons();
        }

        private void SetDataToWeaponInfoView(PlayerWeaponData playerWeaponData)
        {
            var isEquipped = playerWeaponData == _characterWeaponsRepository.CurrentWeaponViewLeft.Value.Data ||
                             playerWeaponData == _characterWeaponsRepository.CurrentWeaponViewRight.Value.Data;

            ControlledEntity.WeaponInfoView.SetWeapon(playerWeaponData, isEquipped,
                _weaponsCharacteristics, _weaponsDataBase,
                _applicationData.PlayerData.CurrentCoinsCount, _resourceShortFormsDataBase);
        }

        private void OnEquipButtonPressedHandler(PlayerWeaponData playerWeaponData)
        {
            if (_isLeftWeaponState)
            {
                _characterWeaponsRepository.CurrentWeaponViewLeft.Value.Destroy();
                _characterWeaponsRepository.CurrentWeaponViewLeft.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.LeftWeaponPlacer, true);
            }
            else
            {
                _characterWeaponsRepository.CurrentWeaponViewRight.Value.Destroy();
                _characterWeaponsRepository.CurrentWeaponViewRight.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.RightWeaponPlacer, false);
            }

            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);

            ResetWeapons();
        }

        private void ResetWeapons()
        {
            ControlledEntity.WeaponCardsView.Clear();
            ControlledEntity.WeaponCardsView.ShowWeaponCards(GetWeaponsForHand(),
                _weaponsService);
        }

        private List<PlayerWeaponData> GetWeaponsForHand()
        {
            var weaponsToCraft = new List<PlayerWeaponData>();
            foreach (var playerWeaponData in _applicationData.PlayerData.WeaponsSaveData)
            {
                if (!_isLeftWeaponState &&
                    _characterWeaponsRepository.CurrentWeaponViewLeft.Value.Data == playerWeaponData)
                {
                    continue;
                }

                if (_isLeftWeaponState &&
                    _characterWeaponsRepository.CurrentWeaponViewRight.Value.Data == playerWeaponData)
                {
                    continue;
                }

                weaponsToCraft.Add(playerWeaponData);
            }

            return weaponsToCraft;
        }

        private void OnWeaponInfoCloseButtonPressedHandler()
        {
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
        }
    }
}
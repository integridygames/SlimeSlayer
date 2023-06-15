using System;
using System.Collections.Generic;
using Game.DataBase;
using Game.DataBase.Essence;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views.UI;
using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class CraftScreenController : ControllerBase<CraftScreenView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly WeaponsService _weaponsService;
        private readonly GameResourceData _gameResourceData;

        private const int CraftPrice = 100;

        public CraftScreenController(CraftScreenView controlledEntity, ApplicationData applicationData,
            WeaponsService weaponsService, GameResourceData gameResourceData) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _weaponsService = weaponsService;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            ControlledEntity.OnShow += OnCraftScreenShowHandler;
            ControlledEntity.OnHide += OnCraftScreenHideHandler;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed += OnWeaponCarsPressedHandler;
            ControlledEntity.OnCraftButtonPressed += OnCraftButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnShow -= OnCraftScreenShowHandler;
            ControlledEntity.OnHide -= OnCraftScreenHideHandler;
            ControlledEntity.WeaponCardsView.OnWeaponCardPressed -= OnWeaponCarsPressedHandler;
            ControlledEntity.OnCraftButtonPressed -= OnCraftButtonPressedHandler;
        }

        private void OnCraftScreenShowHandler()
        {
            FillScreen();
        }

        private void FillScreen()
        {
            var weaponsToCraft = GetWeaponsEnabledToCraft();
            ControlledEntity.WeaponCardsView.ShowWeaponCards(weaponsToCraft, _weaponsService);

            ControlledEntity.IsCraftEnabled = false;
            ControlledEntity.SetCraftPrice(CraftPrice);
        }

        private List<PlayerWeaponData> GetWeaponsEnabledToCraft()
        {
            var weaponsToCraft = new List<PlayerWeaponData>();
            foreach (var playerWeaponData in _applicationData.PlayerData.WeaponsSaveData)
            {
                if (playerWeaponData._guid == _applicationData.PlayerData.CurrentLeftWeaponGuid ||
                    playerWeaponData._guid == _applicationData.PlayerData.CurrentRightWeaponGuid ||
                    playerWeaponData._rarityType == RarityType.Legendary)
                {
                    continue;
                }

                weaponsToCraft.Add(playerWeaponData);
            }

            return weaponsToCraft;
        }


        private void OnCraftScreenHideHandler()
        {
            ClearScreen();
        }

        private void ClearScreen()
        {
            ControlledEntity.WeaponCardsView.Clear();
            ControlledEntity.ClearSlots();
            ControlledEntity.ClearResultWeapon();
        }

        private void OnWeaponCarsPressedHandler(PlayerWeaponData playerWeaponData, WeaponCardView weaponCardView)
        {
            if (ControlledEntity.ContainsWeapon(playerWeaponData))
            {
                if (ControlledEntity.IsSlotsFilled)
                {
                    ControlledEntity.IsCraftEnabled = false;
                    ControlledEntity.ClearResultWeapon();
                }

                weaponCardView.IsFrameEnabled = false;
                ControlledEntity.RemoveWeaponFromSlot(playerWeaponData);
                return;
            }

            if (ControlledEntity.IsSlotsFilled)
            {
                return;
            }

            if (ControlledEntity.ContainsWeaponWithAnotherTypeOrRarity(playerWeaponData))
            {
                return;
            }

            ControlledEntity.AddWeaponToSlot(playerWeaponData, _weaponsService);
            weaponCardView.IsFrameEnabled = true;

            if (ControlledEntity.IsSlotsFilled)
            {
                var nextRarityWeapon = GetNextRarityWeapon(playerWeaponData);
                ControlledEntity.SetResultWeapon(nextRarityWeapon, _weaponsService);

                ControlledEntity.IsCraftEnabled =
                    _gameResourceData.GetResourceQuantity(GameResourceType.Coin) >= CraftPrice;
            }
        }

        private PlayerWeaponData GetNextRarityWeapon(PlayerWeaponData playerWeaponData)
        {
            return new PlayerWeaponData(playerWeaponData._weaponType, playerWeaponData._rarityType + 1);
        }

        private void OnCraftButtonPressedHandler()
        {
            foreach (var guid in ControlledEntity.SlotsGuids)
            {
                var indexToRemove = _applicationData.PlayerData.WeaponsSaveData.FindIndex(x => x._guid == guid);
                _applicationData.PlayerData.WeaponsSaveData.RemoveAt(indexToRemove);
            }

            _applicationData.PlayerData.WeaponsSaveData.Add(ControlledEntity.ResultWeaponData);

            _gameResourceData.RemoveResource(GameResourceType.Coin, CraftPrice);

            ClearScreen();
            FillScreen();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using ModestTree;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Weapons
{
    public class CraftScreenView : ViewBase
    {
        private const int MaxSlotsCount = 4;

        public event Action OnCloseButtonPressed;
        public event Action OnCraftButtonPressed;

        [SerializeField] private WeaponCardsView _weaponCardsView;
        [SerializeField] private List<Transform> _slotsRoots;
        [SerializeField] private Transform _resultWeaponRoot;
        [SerializeField] private UiButton _craftButton;
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private TMP_Text _craftPrice;

        private readonly Dictionary<string, PlayerWeaponData> _slotsData = new(MaxSlotsCount);

        private readonly WeaponCardView[] _weaponCardViewsInSlots =
        {
            null,
            null,
            null,
            null,
        };

        private WeaponCardView _resultWeaponView;

        public PlayerWeaponData ResultWeaponData => _resultWeaponView != null ? _resultWeaponView.WeaponData : null;

        public WeaponCardsView WeaponCardsView => _weaponCardsView;

        public bool IsSlotsFilled => _slotsData.Count == MaxSlotsCount;

        public bool IsCraftEnabled
        {
            set => _craftButton.interactable = value;
        }

        public string[] SlotsGuids { get; } =
        {
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
        };

        public bool ContainsWeapon(PlayerWeaponData playerWeaponData)
        {
            return _slotsData.ContainsKey(playerWeaponData._guid);
        }

        public void AddWeaponToSlot(PlayerWeaponData playerWeaponData, WeaponsService weaponsService)
        {
            if (_slotsData.Count >= MaxSlotsCount)
            {
                Debug.LogError("view contains only 4 slots for craft");
                return;
            }

            if (_slotsRoots.Count != MaxSlotsCount)
            {
                Debug.LogError("view must contain 4 slots for craft");
                return;
            }

            var freeIndex = TakeFirstFreeIndex(playerWeaponData);
            var weaponCard = weaponsService.SpawnWeaponCardMini(_slotsRoots[freeIndex], playerWeaponData, true);

            _weaponCardViewsInSlots[freeIndex] = weaponCard;
            _slotsData.Add(playerWeaponData._guid, playerWeaponData);
        }

        private int TakeFirstFreeIndex(PlayerWeaponData playerWeaponData)
        {
            var index = SlotsGuids.IndexOf(string.Empty);
            SlotsGuids[index] = playerWeaponData._guid;
            return index;
        }

        public void RemoveWeaponFromSlot(PlayerWeaponData playerWeaponData)
        {
            var index = SlotsGuids.IndexOf(playerWeaponData._guid);
            Destroy(_weaponCardViewsInSlots[index].gameObject);

            _weaponCardViewsInSlots[index] = null;
            SlotsGuids[index] = string.Empty;

            _slotsData.Remove(playerWeaponData._guid);
        }

        public void ClearSlots()
        {
            for (var index = 0; index < _weaponCardViewsInSlots.Length; index++)
            {
                var weaponCardView = _weaponCardViewsInSlots[index];
                if (weaponCardView != null)
                {
                    Destroy(weaponCardView.gameObject);
                    _weaponCardViewsInSlots[index] = null;
                    SlotsGuids[index] = string.Empty;
                }
            }

            _slotsData.Clear();
        }

        public void SetResultWeapon(PlayerWeaponData playerWeaponData, WeaponsService weaponsService)
        {
            _resultWeaponView = weaponsService.SpawnWeaponCard(_resultWeaponRoot, playerWeaponData, true);
        }

        public void ClearResultWeapon()
        {
            if (_resultWeaponView != null)
            {
                Destroy(_resultWeaponView.gameObject);
            }
        }

        public void SetCraftPrice(int price)
        {
            _craftPrice.text = $"Craft (<sprite name=IconMoney>{price})";
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _closeButton.OnReleased += OnCloseButtonReleasedHandler;
            _craftButton.OnReleased += OnCraftButtonReleaseHandler;
        }

        protected override void OnDisable()
        {
            _closeButton.OnReleased -= OnCloseButtonReleasedHandler;
            _craftButton.OnReleased -= OnCraftButtonReleaseHandler;

            base.OnDisable();
        }

        private void OnCloseButtonReleasedHandler()
        {
            OnCloseButtonPressed?.Invoke();
        }

        private void OnCraftButtonReleaseHandler()
        {
            OnCraftButtonPressed?.Invoke();
        }

        public bool ContainsWeaponWithAnotherTypeOrRarity(PlayerWeaponData playerWeaponData)
        {
            return _slotsData.Values.Any(x =>
                x._weaponType != playerWeaponData._weaponType || x._rarityType != playerWeaponData._rarityType);
        }
    }
}
using System;
using System.Collections.Generic;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Weapons
{
    public class WeaponCardsView : MonoBehaviour
    {
        public event Action<PlayerWeaponData, WeaponCardView> OnWeaponCardPressed;

        [SerializeField] private Transform _weaponsCardsRoot;

        private readonly List<WeaponCardView> _spawnedWeaponCardViews = new();

        public void ShowWeaponCards(List<PlayerWeaponData> playerWeaponsData, WeaponsService weaponsService)
        {
            foreach (var weaponData in playerWeaponsData)
            {
                var weaponCardView = weaponsService.SpawnWeaponCard(_weaponsCardsRoot,
                    weaponData);

                _spawnedWeaponCardViews.Add(weaponCardView);

                weaponCardView.OnWeaponCardPressed += OnWeaponCardPressed;
            }
        }

        public void Clear()
        {
            foreach (var weaponCardView in _spawnedWeaponCardViews)
            {
                weaponCardView.OnWeaponCardPressed -= OnWeaponCardPressed;

                Destroy(weaponCardView.gameObject);
            }

            _spawnedWeaponCardViews.Clear();
        }
    }
}
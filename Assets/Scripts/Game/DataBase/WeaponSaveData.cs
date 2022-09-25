﻿using System;
using Game.Gameplay.Models.Weapon;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class WeaponSaveData
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private int _cardsCount;
        [SerializeField] private int _level;

        public WeaponType WeaponType => _weaponType;

        public int CardsCount => _cardsCount;

        public int Level => _level;
    }
}
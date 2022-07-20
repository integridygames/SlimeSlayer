using System;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }
    }
}
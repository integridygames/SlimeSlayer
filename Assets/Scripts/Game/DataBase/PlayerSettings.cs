using System;
using TegridyCore;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerSettings
    {
        [SerializeField] private RxField<bool> _vibrationEnabled = true;

        [SerializeField] private RxField<bool> _soundEnabled = true;
        [SerializeField] private RxField<bool> _musicEnabled = true;

        public RxField<bool> VibrationEnabled => _vibrationEnabled;

        public RxField<bool> SoundsEnabled => _soundEnabled;

        public RxField<bool> MusicEnabled => _musicEnabled;
    }
}
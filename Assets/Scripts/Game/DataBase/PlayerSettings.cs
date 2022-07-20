using System;
using TegridyCore;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerSettings
    {
        [SerializeField] private bool _vibrationEnabled = true;

        [SerializeField] private RxField<bool> _soundEnabled = true;
        [SerializeField] private RxField<bool> _musicEnabled = false;
        
        public bool VibrationEnabled
        {
            get => _vibrationEnabled;
            set => _vibrationEnabled = value;
        }
        
        public RxField<bool> SoundsEnabled => _soundEnabled;

        public RxField<bool> MusicEnabled => _musicEnabled;
    }
}
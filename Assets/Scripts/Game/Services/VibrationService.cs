using Game.DataBase;
using Game.Gameplay.Models;
using JetBrains.Annotations;
using Zenject;

namespace Game.Services
{
    [UsedImplicitly]
    public class VibrationService : IInitializable
    {
        private readonly PlayerSettings _playerSettings;

        public VibrationService(ApplicationData applicationData)
        {
            _playerSettings = applicationData.PlayerSettings;
        }

        public void Initialize()
        {
#if !UNITY_EDITOR && !UNITY_STANDALONE
            Vibration.Init();
#endif
        }

        public void Vibrate(int milliseconds)
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;

#if !UNITY_EDITOR && !UNITY_STANDALONE
            Vibration.Vibrate(milliseconds);
#endif
        }

        public void VibratePop()
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;
#if !UNITY_EDITOR && !UNITY_STANDALONE
            Vibration.VibratePop();
#endif
        }

        public void VibrateNope()
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;

#if !UNITY_EDITOR && !UNITY_STANDALONE
            Vibration.VibrateNope();
#endif
        }
    }
}
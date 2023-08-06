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
            Vibration.Init();
        }

        public void Vibrate(int milliseconds)
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;

            Vibration.Vibrate(milliseconds);
        }

        public void VibratePop()
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;

            Vibration.VibratePop();
        }

        public void VibrateNope()
        {
            if (_playerSettings.VibrationEnabled.Value == false) return;

            Vibration.VibrateNope();
        }
    }
}
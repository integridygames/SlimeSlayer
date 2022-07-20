using JetBrains.Annotations;
using Zenject;

namespace Game.Services
{
    [UsedImplicitly]
    public class VibrationService : IInitializable
    {
        public bool IsEnabled { get; set; }

        public void Initialize()
        {
            Vibration.Init();
        }

        public void Vibrate(int milliseconds)
        {
            if (IsEnabled == false) return;

            Vibration.Vibrate(milliseconds);
        }

        public void VibratePop()
        {
            if (IsEnabled == false) return;

            Vibration.VibratePop();
        }

        public void VibrateNope()
        {
            if (IsEnabled == false) return;

            Vibration.VibrateNope();
        }
    }
}
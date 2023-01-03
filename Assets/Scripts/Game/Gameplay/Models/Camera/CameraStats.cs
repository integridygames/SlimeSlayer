namespace Game.Gameplay.Models.Camera
{
    public class CameraStats
    {
        public float CameraSpeed { get; } = 4;

        public float CameraToTargetSpeed { get; } = 1.5f;

        public float InBattleCameraDistance { get; } = 5f;
    }
}
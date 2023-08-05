using UnityEngine;

namespace Game.Gameplay.Models.Camera
{
    public class CameraRepository
    {
        public Vector3 CameraTargetPosition { get; set; }
        public float Speed { get; set; }

        public Vector3 Offset { get; set; }
    }
}
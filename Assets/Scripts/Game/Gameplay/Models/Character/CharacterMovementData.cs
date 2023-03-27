using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterMovementData
    {
        public Vector3 NextPosition { get; set; }

        public Vector3 Direction { get; set; }

        public Vector3 MovingVector { get; set; }
    }
}
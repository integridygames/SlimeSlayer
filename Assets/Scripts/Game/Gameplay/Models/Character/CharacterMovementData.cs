using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterMovementData
    {
        public Vector3 NextPosition { get; set; }

        public Vector3 Direction { get; set; }

        public Vector3 MovingVector { get; set; }

        public void Reset()
        {
            NextPosition = Vector3.zero;
            Direction = Vector3.zero;
            MovingVector = Vector3.zero;
        }
    }
}
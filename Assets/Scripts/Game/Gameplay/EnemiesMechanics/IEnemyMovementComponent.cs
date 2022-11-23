using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyMovementComponent
    {
        public Vector3 Position { get; }

        public void SetTarget(Vector3 position);

        public void UpdateMovement();
    }
}
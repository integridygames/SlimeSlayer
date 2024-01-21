using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyMovementComponent
    {
        public Vector3 Position { get; }

        public Vector3 Target { get; }

        public void UpdateMovement(float speed, bool isOnAttack);
    }
}
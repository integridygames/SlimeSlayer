using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyMovementComponent
    {
        public Vector3 Position { get; set; }
    }
}
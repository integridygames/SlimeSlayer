using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class GradualMovementComponent : IEnemyMovementComponent
    {
        private const float Speed = 500f;

        private readonly Rigidbody _enemyRigidBody;

        public Vector3 Position => _enemyRigidBody.transform.position;

        public Vector3 Target { get; set; }

        public GradualMovementComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void UpdateMovementData()
        {
        }

        public void UpdateMovement()
        {
            var direction = (Target - Position).normalized;

            _enemyRigidBody.velocity = direction * Time.fixedDeltaTime * Speed;
        }
    }
}
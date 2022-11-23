using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class ImpulseMovementComponent : IEnemyMovementComponent
    {
        private readonly Rigidbody _enemyRigidBody;

        public Vector3 Position => _enemyRigidBody.transform.position;

        public ImpulseMovementComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void SetTarget(Vector3 position)
        {

        }

        public void UpdateMovement()
        {

        }
    }
}
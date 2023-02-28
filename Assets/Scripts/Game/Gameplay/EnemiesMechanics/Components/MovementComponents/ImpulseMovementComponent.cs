using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class ImpulseMovementComponent : IEnemyMovementComponent
    {
        private const float ImpulseDuration = 2.5f;
        private const float ImpulseForce = 5f;

        private readonly Rigidbody _enemyRigidBody;

        public Vector3 Position => _enemyRigidBody.transform.position;

        public Vector3 Target { get; set; }

        private float _latestImpulseTime;

        public ImpulseMovementComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void UpdateMovementData()
        {

        }

        public void UpdateMovement()
        {
            if (Time.time - _latestImpulseTime < ImpulseDuration)
            {
                return;
            }

            _latestImpulseTime = Time.time;

            var direction = (Target - Position).normalized;
            direction.y = 0;

            _enemyRigidBody.AddForce(direction * ImpulseForce, ForceMode.Impulse);
        }
    }
}
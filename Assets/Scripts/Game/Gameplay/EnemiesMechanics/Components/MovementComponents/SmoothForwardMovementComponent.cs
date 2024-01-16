using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class SmoothForwardMovementComponent : IEnemyMovementComponent
    {
        private const float RotationSpeed = 2f;
        private const float Acceleration = 10f;

        private readonly Rigidbody _enemyRigidBody;

        private Quaternion _targetRotation;
        private Vector3 _targetVelocity;

        public Vector3 Position => _enemyRigidBody.transform.position;

        public Vector3 Target { get; set; }

        public SmoothForwardMovementComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void UpdateMovementData(float speed)
        {
            var direction = (Target - Position).normalized;
            var forward = _enemyRigidBody.transform.forward;

            direction.y = 0;

            _targetRotation = Quaternion.Lerp(_enemyRigidBody.rotation, Quaternion.LookRotation(direction, Vector3.up),
                Time.deltaTime * RotationSpeed);
            _targetVelocity = Vector3.Lerp(_enemyRigidBody.velocity, forward * speed, Time.deltaTime * Acceleration);
        }

        public void UpdateMovement()
        {
            _enemyRigidBody.rotation = _targetRotation;
            _enemyRigidBody.velocity = _targetVelocity;
        }
    }
}
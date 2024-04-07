using Game.Gameplay.Views.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class SmoothForwardMovementComponent : IEnemyMovementComponent
    {
        private const float RotationSpeed = 2f;
        private const float Acceleration = 10f;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly CharacterView _characterView;

        public Vector3 Position => _navMeshAgent.transform.position;
        public Vector3 Target => _navMeshAgent.destination;

        public SmoothForwardMovementComponent(NavMeshAgent navMeshAgent, CharacterView characterView)
        {
            _navMeshAgent = navMeshAgent;
            _characterView = characterView;
        }

        public void UpdateMovement(float speed, bool isOnAttack)
        {
            if (isOnAttack)
            {
                Move(0);
                return;
            }

            Move(speed);
        }

        private void Move(float speed)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.destination = _characterView.transform.position;
            _navMeshAgent.acceleration = Acceleration;
            _navMeshAgent.angularSpeed = RotationSpeed;

            if (speed == 0) return;
            
            var turnTowardNavSteeringTarget = _navMeshAgent.steeringTarget;
            var direction = (turnTowardNavSteeringTarget - _navMeshAgent.transform.position).normalized;

            if (direction is { x: 0, z: 0 }) return;
                
            var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
}
using Game.Gameplay.Views.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.EnemiesMechanics.Components.MovementComponents
{
    public class MoveToRandomPointAtRadiusComponent : IEnemyMovementComponent
    {
        private const float RotationSpeed = 2f;
        private const float Acceleration = 10f;

        private readonly NavMeshAgent _navMeshAgent;
        private readonly CharacterView _characterView;

        public Vector3 Position => _navMeshAgent.transform.position;
        public Vector3 Target => _navMeshAgent.destination;

        private Vector3? _currentDestinationOffset;

        public MoveToRandomPointAtRadiusComponent(NavMeshAgent navMeshAgent, CharacterView characterView)
        {
            _navMeshAgent = navMeshAgent;
            _characterView = characterView;
        }

        public void UpdateMovement(float speed, bool isOnAttack)
        {
            if (isOnAttack)
            {
                _currentDestinationOffset = null;
                Move(0);
                return;
            }

            if (_currentDestinationOffset == null)
            {
                UpdateDestinationPoint();
            }


            Move(speed);
        }

        private void UpdateDestinationPoint()
        {
            _currentDestinationOffset = Random.insideUnitCircle * 5f;
        }

        private void Move(float speed)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.acceleration = Acceleration;
            _navMeshAgent.angularSpeed = RotationSpeed;

            if (_currentDestinationOffset != null)
            {
                _navMeshAgent.destination = new Vector3(_characterView.transform.position.x + _currentDestinationOffset.Value.x, 0, _characterView.transform.position.z + _currentDestinationOffset.Value.y);
            }

            if (speed != 0)
            {
                var turnTowardNavSteeringTarget = _navMeshAgent.steeringTarget;
                var direction = (turnTowardNavSteeringTarget - _navMeshAgent.transform.position).normalized;
                var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

                _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, lookRotation, Time.deltaTime * 5);
            }
        }
    }
}
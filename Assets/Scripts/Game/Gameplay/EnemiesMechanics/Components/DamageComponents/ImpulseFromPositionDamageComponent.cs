using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.DamageComponents
{
    public class ImpulseFromPositionDamageComponent : IEnemyDamageComponent
    {
        private const int MaxVelocity = 10;
        private readonly Rigidbody _enemyRigidBody;

        public ImpulseFromPositionDamageComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void Hit(HitInfo hitInfo)
        {
            var hitInfoImpulseDirection = hitInfo.ImpulseDirection;
            hitInfoImpulseDirection.y = 0;

            if (_enemyRigidBody.velocity.magnitude < MaxVelocity)
            {
                _enemyRigidBody.AddForce(hitInfoImpulseDirection * Mathf.Clamp(hitInfo.Damage * 15, 0, MaxVelocity),
                    ForceMode.Impulse);
            }
        }
    }
}
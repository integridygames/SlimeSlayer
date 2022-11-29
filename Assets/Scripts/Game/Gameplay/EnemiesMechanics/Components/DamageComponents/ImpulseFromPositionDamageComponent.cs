using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.DamageComponents
{
    public class ImpulseFromPositionDamageComponent : IEnemyDamageComponent
    {
        private readonly Rigidbody _enemyRigidBody;

        public ImpulseFromPositionDamageComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void Hit(HitInfo hitInfo)
        {
            _enemyRigidBody.AddForce(hitInfo.ImpulseDirection * hitInfo.Damage / 4, ForceMode.Impulse);
        }
    }
}
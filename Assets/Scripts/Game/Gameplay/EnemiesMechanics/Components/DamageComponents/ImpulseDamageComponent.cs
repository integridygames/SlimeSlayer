using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.DamageComponents
{
    public class ImpulseDamageComponent : IEnemyDamageComponent
    {
        private readonly Rigidbody _enemyRigidBody;

        public ImpulseDamageComponent(Rigidbody enemyRigidBody)
        {
            _enemyRigidBody = enemyRigidBody;
        }

        public void Hit(Vector3 sourceDirection)
        {

        }
    }
}
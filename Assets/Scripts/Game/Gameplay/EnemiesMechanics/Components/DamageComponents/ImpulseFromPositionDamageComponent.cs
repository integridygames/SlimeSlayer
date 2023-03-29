using Game.DataBase.FX;
using Game.Gameplay.Factories;
using Game.Gameplay.Views.FX;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.DamageComponents
{
    public class ImpulseFromPositionDamageComponent : IEnemyDamageComponent
    {
        private const int MaxVelocity = 10;
        private readonly Rigidbody _enemyRigidBody;
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;
        private readonly RecyclableParticleType _damageFxType;

        public ImpulseFromPositionDamageComponent(Rigidbody enemyRigidBody,
            RecyclableParticlesPoolFactory recyclableParticlesPoolFactory, RecyclableParticleType damageFxType)
        {
            _enemyRigidBody = enemyRigidBody;
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _damageFxType = damageFxType;
        }

        public void Hit(HitInfo hitInfo, int looseDestructionStatesCount)
        {
            var hitInfoImpulseDirection = hitInfo.ImpulseDirection;
            hitInfoImpulseDirection.y = 0;

            DoDamageFx(hitInfo, hitInfoImpulseDirection, (int) (looseDestructionStatesCount * hitInfo.Damage * 10)); // TODO заменить 8 на значение индивидуальное для каждого оружия/снаряда

            if (_enemyRigidBody.velocity.magnitude < MaxVelocity)
            {
                _enemyRigidBody.AddForce(hitInfoImpulseDirection * Mathf.Clamp(hitInfo.Damage * 15, 0, MaxVelocity),
                    ForceMode.Impulse);
            }
        }

        private void DoDamageFx(HitInfo hitInfo, Vector3 hitInfoImpulseDirection, int count)
        {
            var damageFxView = _recyclableParticlesPoolFactory.GetElement(_damageFxType);
            if (damageFxView != null)
            {
                damageFxView.transform.position = hitInfo.HitPosition;
                damageFxView.transform.rotation = Quaternion.LookRotation(hitInfoImpulseDirection);

                damageFxView.OnParticleSystemStopped += OnDamageFxStoppedHandler;

                damageFxView.Emit(count);
            }
        }

        private void OnDamageFxStoppedHandler(RecyclableParticleView damageFx)
        {
            damageFx.OnParticleSystemStopped -= OnDamageFxStoppedHandler;

            _recyclableParticlesPoolFactory.RecycleElement(_damageFxType, damageFx);
        }
    }
}
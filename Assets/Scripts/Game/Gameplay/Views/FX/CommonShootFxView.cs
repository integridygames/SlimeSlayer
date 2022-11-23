using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Weapon;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class CommonShootFxView : RecyclableParticleView
    {
        public event Action<CommonShootFxView, EnemyViewBase, Vector3> OnEnemyCollide;

        private List<ParticleCollisionEvent> _collisionEvents;

        public WeaponType WeaponType { get; private set; }

        public void Initialize(WeaponType weaponType)
        {
            WeaponType = weaponType;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EnemyViewBase enemyView))
            {
                _particleSystem.GetCollisionEvents(other, _collisionEvents);

                OnEnemyCollide?.Invoke(this, enemyView, _collisionEvents.First().intersection);
            }
        }
    }
}
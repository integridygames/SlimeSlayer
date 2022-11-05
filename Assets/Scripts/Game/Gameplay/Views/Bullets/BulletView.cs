using System;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ProjectileViewBase
    {
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        private Action<EnemyView, BulletView> _enemyCollideHandler;

        public void SetEnemyCollideHandler(Action<EnemyView, BulletView> enemyCollideHandler)
        {
            _enemyCollideHandler = enemyCollideHandler;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView enemyView))
            {
                _enemyCollideHandler?.Invoke(enemyView, this);
            }
        }

        public override void Recycle()
        {
            base.Recycle();
            Rigidbody.velocity = Vector3.zero;
        }
    }
}
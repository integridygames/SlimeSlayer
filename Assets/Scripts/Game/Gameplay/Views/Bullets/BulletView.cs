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

        [SerializeField] private TrailRenderer _trailRenderer;

        private Action<BulletView, EnemyViewBase> _onEnemyCollide;

        public override void Shoot()
        {
            Rigidbody.velocity = Direction * Force;
        }

        public void SetCollideAction(Action <BulletView, EnemyViewBase> onEnemyCollide)
        {
            _onEnemyCollide = onEnemyCollide;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyView = other.GetComponentInParent<EnemyViewBase>();

            if (enemyView != null)
            {
                _onEnemyCollide?.Invoke(this, enemyView);
            }
        }

        public override void Recycle()
        {
            _onEnemyCollide = null;
            _trailRenderer.Clear();
            base.Recycle();
            Rigidbody.velocity = Vector3.zero;
        }
    }
}
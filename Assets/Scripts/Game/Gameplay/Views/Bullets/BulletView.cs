using System;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ProjectileViewBase
    {
        public event Action<BulletView, EnemyViewBase> OnEnemyCollide;

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        [SerializeField] private TrailRenderer _trailRenderer;

        public override void Shoot()
        {
            Rigidbody.velocity = Direction * Force;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyView = other.GetComponentInParent<EnemyViewBase>();

            if (enemyView != null)
            {
                OnEnemyCollide?.Invoke(this, enemyView);
            }
        }

        public override void Recycle()
        {
            _trailRenderer.Clear();
            base.Recycle();
            Rigidbody.velocity = Vector3.zero;
        }
    }
}
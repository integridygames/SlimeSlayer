using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Views.Character;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    public class FireBallView : ProjectileViewBase
    {
        private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        private Action<FireBallView> _playerCollide;

        private CancellationTokenSource _cancellationTokenSource;

        public override void Shoot()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            ShootFireBallAsync().Forget();
        }

        private async UniTaskVoid ShootFireBallAsync()
        {
            await UniTask.WaitForSeconds(0.2f, cancellationToken: _cancellationTokenSource.Token);

            if (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                return;
            }

            Rigidbody.velocity = Direction * Force;
        }

        public void SetPlayerCollideAction(Action<FireBallView> onPlayerCollide)
        {
            _playerCollide = onPlayerCollide;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyView = other.GetComponentInParent<CharacterView>();

            if (enemyView != null)
            {
                _playerCollide?.Invoke(this);
            }
        }

        public override void Recycle()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            base.Recycle();
            Rigidbody.velocity = Vector3.zero;
        }
    }
}
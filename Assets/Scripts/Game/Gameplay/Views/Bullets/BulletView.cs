using System;
using Game.DataBase.Weapon;
using Game.Gameplay.Views.Enemy;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets 
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ViewBase
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private float _lifeTime;

        private Action<EnemyView, BulletView> _enemyCollideHandler;

        public float CurrentLifeTime { get; private set; }

        public float LifeTime => _lifeTime;
        public Rigidbody Rigidbody => _rigidbody;
        public BulletType BulletType => _bulletType;

        public void AddToCurrentLifeTime(float time)
        {
            CurrentLifeTime += time;
        }

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

        protected override void OnDisable()
        {
            base.OnDisable();

            CurrentLifeTime = 0;
        }
    }
}
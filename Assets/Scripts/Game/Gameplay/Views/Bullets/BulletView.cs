using System;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Enemy;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets 
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ViewBase
    {
        public event Action<EnemyView, BulletView> OnBulletCollide;
        
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private float _lifeTime;

        private float _currentLifeTime = 0;
        public float CurrentLifeTime => _currentLifeTime;
        public float LifeTime => _lifeTime;
        public Rigidbody Rigidbody => _rigidbody;
        public WeaponType WeaponType => _weaponType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView enemyView)) 
            {
                OnBulletCollide?.Invoke(enemyView, this);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _currentLifeTime = 0;
        }

        public void AddToCurrentLifeTime(float time) 
        {
            _currentLifeTime += time;
        }
    }
}
using System;
using Game.Gameplay.Utils.Weapons;
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
        [SerializeField] private float _damage;

        private float _currentLifeTime = 0;
        public float CurrentLifeTime => _currentLifeTime;
        public float LifeTime => _lifeTime;
        public Rigidbody Rigidbody => _rigidbody;
        public WeaponType WeaponType => _weaponType;
        public float Damage => _damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView enemyView)) 
            {
                OnBulletCollide?.Invoke(enemyView, this);
            }
        }

        public void Recycle() 
        {
            _currentLifeTime = 0;
            gameObject.SetActive(false);
        }

        public void AddToCurrentLifeTime(float time) 
        {
            _currentLifeTime += time;
        }
    }
}
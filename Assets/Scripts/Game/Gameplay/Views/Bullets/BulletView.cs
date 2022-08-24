using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets 
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ViewBase
    {
        [SerializeField] private string _identificator;
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private float _lifeTime;
        [SerializeField] private Rigidbody _rigidbody;

        private float _currentLifeTime = 0;

        public float CurrentLifeTime => _currentLifeTime;
        public string ID => _identificator;
        public float LifeTime => _lifeTime;
        public Rigidbody Rigidbody => _rigidbody;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyView>(out EnemyView enemyView)) 
            {
                enemyView.TakeDamage(_weaponView.DamageValue);               
            }
            Die();
        }

        public void Die() 
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
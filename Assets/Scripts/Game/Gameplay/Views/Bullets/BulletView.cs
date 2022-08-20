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

        private float _currentLifeTime = 0;

        public float CurrentLifeTime => _currentLifeTime;
        public string ID => _identificator;
        public float LifeTime => _lifeTime;

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
            Destroy(gameObject);
        }

        public void AddToCurrentLifeTime(float time) 
        {
            _currentLifeTime += time;
        }
    }
}
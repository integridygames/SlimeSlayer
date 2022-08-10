using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets 
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : ViewBase
    {
        [SerializeField] private string Identificator;
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private float _lifeTime;

        public float CurrentLifeTime = 0;

        public string ID => Identificator;
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
    }
}
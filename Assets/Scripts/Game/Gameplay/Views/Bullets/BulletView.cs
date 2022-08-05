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

        public string ID => Identificator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyView>(out EnemyView enemyView)) 
            {
                enemyView.TakeDamage(_weaponView.DamageValue);               
            }
            Destroy(gameObject);
        }
    }
}
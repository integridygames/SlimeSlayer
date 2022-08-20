using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Views.Weapons 
{
    public abstract class WeaponView : ViewBase
    {
        [SerializeField] protected Utils.Weapons.Weapons Identificator;
        [SerializeField] protected float Damage;
        [SerializeField] protected float Duration;
        [SerializeField] protected BulletView BulletPrefab;
        [SerializeField] protected Transform ShootingPoint;
        [SerializeField] protected float ShootingForce;
        [SerializeField] protected bool IsAmmoUnlimited;
        [SerializeField] protected float MaxAmmoQunatity;

        private float _currentAmmoQuantity;

        public float CurrentAmmoQuantity => _currentAmmoQuantity;
        public int ID => (int)Identificator;
        public float DamageValue => Damage;
        public float DurationValue => Duration;
        public Transform ShootingPointTranform => ShootingPoint;
        public bool IsUnlimited => IsAmmoUnlimited;
        public float MaxBulletsQunatity => MaxAmmoQunatity;

        public virtual BulletView Shoot()
        {
            var bullet = Instantiate(BulletPrefab, ShootingPoint.transform.position, Quaternion.identity);
            var rigibody = bullet.GetComponent<Rigidbody>();

            rigibody.velocity = transform.forward * ShootingForce;
            return bullet;
        }

        public void AddAmmo(float quantity) 
        {
            float currentQuantity = _currentAmmoQuantity + quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, MaxAmmoQunatity);
            _currentAmmoQuantity = currentQuantity;
        }

        public void RemoveAmmo(float quantity) 
        {
            float currentQuantity = _currentAmmoQuantity - quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, MaxAmmoQunatity);
            _currentAmmoQuantity = currentQuantity;
        }
    }
}
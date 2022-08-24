using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Utils.Weapons;

namespace Game.Gameplay.Views.Weapons 
{
    public abstract class WeaponView : ViewBase
    {
        [SerializeField] protected WeaponsEnum Identificator;
        [SerializeField] protected float Damage;
        [SerializeField] protected float Duration;
        [SerializeField] protected BulletView BulletPrefab;
        [SerializeField] protected Transform ShootingPoint;
        [SerializeField] protected float ShootingForce;
        [SerializeField] protected bool IsAmmoUnlimited;
        [SerializeField] protected float MaxAmmoQunatity;
    
        public float CurrentAmmoQuantity { get; private set; }
        public WeaponsEnum ID => Identificator;
        public float DamageValue => Damage;
        public float DurationValue => Duration;
        public Transform ShootingPointTranform => ShootingPoint;
        public bool IsUnlimited => IsAmmoUnlimited;
        public float MaxBulletsQunatity => MaxAmmoQunatity;
        public BulletView BulletTemplate => BulletPrefab;

        public virtual BulletView Shoot(BulletView bullet)
        {
            bullet.transform.position = ShootingPoint.transform.position;
            bullet.gameObject.SetActive(true);
            bullet.Rigidbody.velocity = transform.forward * ShootingForce;     
            return bullet;
        }

        public void AddAmmo(float quantity) 
        {
            float currentQuantity = CurrentAmmoQuantity + quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, MaxAmmoQunatity);
            CurrentAmmoQuantity = currentQuantity;
        }

        public void RemoveAmmo(float quantity) 
        {
            float currentQuantity = CurrentAmmoQuantity - quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, MaxAmmoQunatity);
            CurrentAmmoQuantity = currentQuantity;
        }
    }
}
using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Utils.Weapons;

namespace Game.Gameplay.Views.Weapons
{
    public abstract class WeaponViewBase : ViewBase
    {
        [SerializeField] protected WeaponType _id;
        [SerializeField] protected float _duration;
        [SerializeField] protected Transform _shootingPoint;
        [SerializeField] protected float _shootingForce;
        [SerializeField] protected bool _isAmmoUnlimited;
        [SerializeField] protected float _maxAmmoQuantity;

        public float CurrentAmmoQuantity { get; private set; }
        public WeaponType ID => _id;
        public float Duration => _duration;
        public Transform ShootingPoint => _shootingPoint;
        public bool IsAmmoUnlimited => _isAmmoUnlimited;
        public float MaxBulletsQuantity => _maxAmmoQuantity;

        public virtual BulletView Shoot(BulletView bullet)
        {
            bullet.transform.position = _shootingPoint.transform.position;
            bullet.gameObject.SetActive(true);
            bullet.Rigidbody.velocity = transform.forward * _shootingForce;
            return bullet;
        }

        public void AddAmmo(float quantity)
        {
            float currentQuantity = CurrentAmmoQuantity + quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, _maxAmmoQuantity);
            CurrentAmmoQuantity = currentQuantity;
        }

        public void RemoveAmmo(float quantity)
        {
            float currentQuantity = CurrentAmmoQuantity - quantity;
            currentQuantity = Mathf.Clamp(currentQuantity, 0, _maxAmmoQuantity);
            CurrentAmmoQuantity = currentQuantity;
        }
    }
}
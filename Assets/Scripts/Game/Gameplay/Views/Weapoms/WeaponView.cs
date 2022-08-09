using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Views.Weapons 
{
    public abstract class WeaponView : ViewBase
    {
        [SerializeField] protected string Identificator;
        [SerializeField] protected float Damage;
        [SerializeField] protected float Duration;
        [SerializeField] protected BulletView BulletPrefab;
        [SerializeField] protected Transform ShootingPoint;
        [SerializeField] protected float ShootingForce;

        public string ID => Identificator;
        public float DamageValue => Damage;
        public float DurationValue => Duration;
        public Transform ShootingPointTranform => ShootingPoint;

        public virtual void Shoot()
        {
            var bullet = Instantiate(BulletPrefab, ShootingPoint.transform.position, Quaternion.identity);
            var rigibody = bullet.GetComponent<Rigidbody>();

            rigibody.AddForce(transform.forward * ShootingForce, ForceMode.Impulse);
        }
    }
}
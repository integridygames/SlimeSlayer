using Game.Gameplay.Views.Bullets;
using UnityEngine;

namespace Game.Gameplay.Views.Weapons.Pistols
{
    public class PistolView : WeaponViewBase
    {
        [SerializeField] protected Transform _shootingPoint;
        [SerializeField] protected float _shootingForce;

        public void Shoot(BulletView bulletView)
        {
            bulletView.transform.position = _shootingPoint.transform.position;
            bulletView.gameObject.SetActive(true);
            bulletView.Rigidbody.velocity = transform.forward * _shootingForce;
        }
    }
}
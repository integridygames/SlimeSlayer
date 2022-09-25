using UnityEngine;

namespace Game.Gameplay.Views.Weapons.Pistols
{
    public class PistolView : WeaponViewBase
    {
        [SerializeField] protected Transform _shootingPoint;

        public Transform ShootingPoint
        {
            get => _shootingPoint;
            set => _shootingPoint = value;
        }
    }
}
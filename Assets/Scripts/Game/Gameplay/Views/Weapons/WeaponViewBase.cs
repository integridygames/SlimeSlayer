using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Weapons
{
    public class WeaponViewBase : ViewBase
    {
        [SerializeField] protected Transform _shootingPoint;

        public Transform ShootingPoint
        {
            get => _shootingPoint;
            set => _shootingPoint = value;
        }
    }
}
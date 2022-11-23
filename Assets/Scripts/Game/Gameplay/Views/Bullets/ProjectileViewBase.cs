using Game.DataBase.Weapon;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    public class ProjectileViewBase : ViewBase
    {
        [SerializeField] private ProjectileType _projectileType;

        public ProjectileType ProjectileType => _projectileType;

        [SerializeField] private float _lifeTime;

        public float StartLifeTime => _lifeTime;
        public float CurrentLifeTime { get; private set; }

        public WeaponType WeaponType { get; private set; }

        public void Initialize(WeaponType weaponType)
        {
            WeaponType = weaponType;
        }

        public void AddToCurrentLifeTime(float time)
        {
            CurrentLifeTime += time;
        }

        public virtual void Recycle()
        {
            CurrentLifeTime = 0;
        }
    }
}
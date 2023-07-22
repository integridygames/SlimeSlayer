using Game.DataBase.Weapon;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    public abstract class ProjectileViewBase : ViewBase
    {
        [SerializeField] private ProjectileType _projectileType;

        public ProjectileType ProjectileType => _projectileType;

        [SerializeField] private float _lifeTime;


        public float StartLifeTime => _lifeTime;
        public float CurrentLifeTime { get; private set; }

        public PlayerWeaponData PlayerWeaponData { get; private set; }

        public Vector3 Direction { get; private set; }
        public float Force { get; private set; }
        public bool CanBeMultiple { get; set; }

        public void Initialize(PlayerWeaponData playerWeaponData, Vector3 direction, float force, bool canBeMultiple)
        {
            CanBeMultiple = canBeMultiple;
            Force = force;
            Direction = direction;
            PlayerWeaponData = playerWeaponData;
        }

        public abstract void Shoot();

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
using System.Collections.Generic;
using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;

namespace Game.Gameplay.Systems.Weapon 
{
    public class ProjectilesDestroyerSystem : IUpdateSystem
    {
        private readonly ActiveProjectilesContainer _activeProjectilesContainer;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly List<ProjectileViewBase> _bulletsForDestroy = new();

        public ProjectilesDestroyerSystem(ActiveProjectilesContainer activeProjectilesContainer, WeaponMechanicsService weaponMechanicsService)
        {
            _activeProjectilesContainer = activeProjectilesContainer;
            _weaponMechanicsService = weaponMechanicsService;
        }

        public void Update()
        {
            CalculateLifeTime(Time.deltaTime);
        }

        private void CalculateLifeTime(float deltaTime) 
        {
            foreach (var bulletView in _activeProjectilesContainer.ActiveProjectiles)
            {
                bulletView.AddToCurrentLifeTime(deltaTime);

                if (CheckIfLifeTimeIsOver(bulletView))
                    _bulletsForDestroy.Add(bulletView);
            }

            foreach (var bulletView in _bulletsForDestroy)
            {
                DestroyProjectile(bulletView);
            }

            _bulletsForDestroy.Clear();
        }

        private bool CheckIfLifeTimeIsOver(ProjectileViewBase projectile)
        {
            return projectile.CurrentLifeTime >= projectile.StartLifeTime && projectile != null;
        }

        private void DestroyProjectile(ProjectileViewBase projectileViewBase)
        {
            _weaponMechanicsService.RecycleProjectile(projectileViewBase);
        }
    }  
}
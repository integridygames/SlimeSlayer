using System;
using System.Collections.Generic;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.Models.Bullets
{
    public class ActiveBulletsContainer
    {
        public event Action<EnemyView, BulletView> OnBulletCollide;

        private readonly List<BulletView> _activeBullets = new();

        public IReadOnlyList<BulletView> ActiveBullets => _activeBullets;

        public void AddBullet(BulletView bulletView)
        {
            bulletView.OnBulletCollide += OnBulletCollide;

            _activeBullets.Add(bulletView);
        }

        public void RemoveBullet(BulletView bulletView)
        {
            bulletView.OnBulletCollide -= OnBulletCollide;

            _activeBullets.Remove(bulletView);
        }
    }
}
using System.Collections.Generic;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Models.Weapon
{
    public class ActiveBulletsContainer
    {
        private readonly List<BulletView> _activeBullets = new();

        public IReadOnlyList<BulletView> ActiveBullets => _activeBullets;

        public void AddBullet(BulletView bulletView)
        {
            _activeBullets.Add(bulletView);
        }

        public void RemoveBullet(BulletView bulletView)
        {
            _activeBullets.Remove(bulletView);
        }
    }
}
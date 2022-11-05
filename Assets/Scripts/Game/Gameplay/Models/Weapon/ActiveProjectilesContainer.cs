using System.Collections.Generic;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Models.Weapon
{
    public class ActiveProjectilesContainer
    {
        private readonly List<ProjectileViewBase> _activeProjectiles = new();

        public IReadOnlyList<ProjectileViewBase> ActiveProjectiles => _activeProjectiles;

        public void AddProjectile(ProjectileViewBase projectileView)
        {
            _activeProjectiles.Add(projectileView);
        }

        public void RemoveProjectile(ProjectileViewBase projectileView)
        {
            _activeProjectiles.Remove(projectileView);
        }
    }
}
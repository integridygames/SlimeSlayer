using UnityEngine;

namespace Game.Gameplay.WeaponMechanics
{
    public interface IShootPossibilityComponent
    {
        bool TryToGetTargetCollider(out Collider currentTarget);
        void HandleShoot();
    }
}
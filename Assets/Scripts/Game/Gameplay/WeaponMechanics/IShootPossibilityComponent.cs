using Game.Gameplay.EnemiesMechanics;

namespace Game.Gameplay.WeaponMechanics
{
    public interface IShootPossibilityComponent
    {
        bool TryToGetTargetCollider(out EnemyBase currentTarget);
        void HandleShoot();
    }
}
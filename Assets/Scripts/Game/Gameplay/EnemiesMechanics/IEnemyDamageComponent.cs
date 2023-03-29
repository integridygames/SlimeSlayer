using Game.Gameplay.WeaponMechanics;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyDamageComponent
    {
        public void Hit(HitInfo hitInfo, int looseDestructionStatesCount);
    }
}
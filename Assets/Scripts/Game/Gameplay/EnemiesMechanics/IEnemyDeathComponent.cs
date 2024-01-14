using System;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyDeathComponent
    {
        event Action OnDied;
        void BeginDie();
    }
}
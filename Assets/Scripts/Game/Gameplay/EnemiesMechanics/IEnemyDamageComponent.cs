using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyDamageComponent
    {
        public void Hit(Vector3 sourceDirection);
    }
}
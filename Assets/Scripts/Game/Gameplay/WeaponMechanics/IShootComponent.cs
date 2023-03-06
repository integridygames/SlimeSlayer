using UnityEngine;

namespace Game.Gameplay.WeaponMechanics
{
    public interface IShootComponent
    {
        void Shoot(Vector3 direction);
    }
}
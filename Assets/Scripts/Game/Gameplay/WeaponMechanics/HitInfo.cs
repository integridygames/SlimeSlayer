using UnityEngine;

namespace Game.Gameplay.WeaponMechanics
{
    public struct HitInfo
    {
        public float Damage;
        public Vector3 ImpulseDirection;

        public HitInfo(float damage, Vector3 impulseDirection)
        {
            Damage = damage;
            ImpulseDirection = impulseDirection;
        }
    }
}
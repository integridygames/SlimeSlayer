using UnityEngine;

namespace Game.Gameplay.WeaponMechanics
{
    public struct HitInfo
    {
        public float Damage;
        public Vector3 ImpulseDirection;
        public Vector3 HitPosition;

        public HitInfo(float damage, Vector3 impulseDirection, Vector3 hitPosition)
        {
            Damage = damage;
            ImpulseDirection = impulseDirection;
            HitPosition = hitPosition;
        }
    }
}
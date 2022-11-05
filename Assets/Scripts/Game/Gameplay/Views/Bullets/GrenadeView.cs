using System;
using UnityEngine;

namespace Game.Gameplay.Views.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeView : ProjectileViewBase
    {
        public event Action<GrenadeView> OnRecycle;

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public override void Recycle()
        {
            OnRecycle?.Invoke(this);
            base.Recycle();
        }
    }
}
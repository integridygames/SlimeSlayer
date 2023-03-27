using Game.Gameplay.Views.FX;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Weapons
{
    public class WeaponViewBase : ViewBase
    {
        [SerializeField] protected Transform _shootingPoint;
        [SerializeField] private EmittedParticleView[] _emittedParticleViews;

        public Transform ShootingPoint
        {
            get => _shootingPoint;
            set => _shootingPoint = value;
        }
        public void EmitMuzzleFlash()
        {
            foreach (var emittedParticleView in _emittedParticleViews)
            {
                emittedParticleView.Emit();
            }
        }
    }
}
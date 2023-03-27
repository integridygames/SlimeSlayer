using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class EmittedParticleView : ViewBase
    {
        [SerializeField] protected ParticleSystem _particleSystem;

        public void Emit(int count = 1)
        {
            _particleSystem.Emit(count);
        }
    }
}
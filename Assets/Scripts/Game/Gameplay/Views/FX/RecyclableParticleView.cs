using System;
using Game.DataBase.FX;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class RecyclableParticleView : ViewBase
    {
        public event Action<RecyclableParticleView> OnParticleSystemStopped;

        [SerializeField] protected ParticleSystem _particleSystem;
        [SerializeField] private RecyclableParticleType _particleType;

        public RecyclableParticleType ParticleType => _particleType;

        public void Emit(int count)
        {
            _particleSystem.Emit(count);
        }

        public void Play()
        {
            _particleSystem.Play();
        }

        private void Update()
        {
            if (_particleSystem.isPlaying == false)
            {
                OnParticleSystemStopped?.Invoke(this);
            }
        }
    }
}
using System;
using Game.DataBase.FX;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class RecyclableParticleView : ViewBase
    {
        public event Action<RecyclableParticleView> OnParticleCompletelyStopped;
        public event Action<RecyclableParticleView> OnMainParticleStopped;

        [SerializeField] protected ParticleSystem _particleSystem;
        [SerializeField] private RecyclableParticleType _particleType;

        public RecyclableParticleType ParticleType => _particleType;

        private bool _completelyStopped;
        private bool _mainParticleStopped;

        public void Emit(int count)
        {
            _particleSystem.Emit(count);
            _completelyStopped = false;
            _mainParticleStopped = false;
        }

        public void Play()
        {
            _particleSystem.Play();
            _completelyStopped = false;
            _mainParticleStopped = false;
        }

        private void Update()
        {
            if (_completelyStopped == false && _particleSystem.isPlaying == false)
            {
                if (_mainParticleStopped == false)
                {
                    OnMainParticleStopped?.Invoke(this);
                    _mainParticleStopped = true;
                }

                if (_particleSystem.subEmitters.subEmittersCount == 0)
                {
                    OnParticleCompletelyStopped?.Invoke(this);
                    _completelyStopped = true;
                }
            }
        }
    }
}
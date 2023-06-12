using System;
using Game.Gameplay.Views.FX;
using TegridyUtils.Attributes;

namespace Game.DataBase.FX
{
    [Serializable]
    public class RecyclableParticleRecord
    {
        [ArrayKey]
        public RecyclableParticleType _recyclableParticleType;
        public RecyclableParticleView _recyclableParticleView;
    }
}
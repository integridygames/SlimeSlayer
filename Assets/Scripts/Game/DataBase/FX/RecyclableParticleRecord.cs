using System;
using Game.Gameplay.Views.FX;

namespace Game.DataBase.FX
{
    [Serializable]
    public class RecyclableParticleRecord
    {
        public RecyclableParticleType _recyclableParticleType;
        public RecyclableParticleView _recyclableParticleView;
    }
}
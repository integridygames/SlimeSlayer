using Game.DataBase.FX;
using Game.Gameplay.Views.Container;
using Game.Gameplay.Views.FX;
using UnityEngine;

namespace Game.Gameplay.Factories
{
    public class RecyclableParticlesPoolFactory : PoolFactoryBase<RecyclableParticleView, RecyclableParticleType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly RecyclableParticlesDataBase _recyclableParticlesDataBase;

        public RecyclableParticlesPoolFactory(PoolContainerView poolContainerView,
            RecyclableParticlesDataBase recyclableParticlesDataBase)
        {
            _poolContainerView = poolContainerView;
            _recyclableParticlesDataBase = recyclableParticlesDataBase;
        }

        protected override RecyclableParticleView CreateNewElement(RecyclableParticleType key)
        {
            var recyclableParticleView = _recyclableParticlesDataBase.GetRecordByType(key)._recyclableParticleView;

            return Object.Instantiate(recyclableParticleView, _poolContainerView.transform);
        }

        protected override void OnRecycleInternal(RecyclableParticleView elementView)
        {
            elementView.transform.SetParent(_poolContainerView.transform);
        }
    }
}
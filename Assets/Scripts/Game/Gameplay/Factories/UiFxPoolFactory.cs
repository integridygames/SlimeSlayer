using Game.Gameplay.Views;
using Game.Gameplay.Views.UI;
using UnityEngine;

namespace Game.Gameplay.Factories
{
    //TODO надо убрать int, он бесполезен и загоушка. Фабрика для одного типа итемов.
    public class UiFxPoolFactory : PoolFactoryBase<DamageFx, int>
    {
        private readonly DamageFx _damageFxPrefab;
        private readonly CanvasView _canvasView;

        public UiFxPoolFactory(DamageFx damageFxPrefab, CanvasView canvasView)
        {
            _damageFxPrefab = damageFxPrefab;
            _canvasView = canvasView;
        }

        protected override DamageFx CreateNewElement(int key)
        {
            return Object.Instantiate(_damageFxPrefab, _canvasView.CanvasPoolRoot);
        }
    }
}
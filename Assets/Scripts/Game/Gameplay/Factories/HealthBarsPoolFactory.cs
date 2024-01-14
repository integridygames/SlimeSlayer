using System;
using Game.DataBase;
using Game.Gameplay.Views;
using Game.Gameplay.Views.UI;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class HealthBarsPoolFactory : PoolFactoryBase<EnemyHealthView, HealthBarType>
    {
        private readonly HealthBarsDataBase _healthBarsDataBase;
        private readonly CanvasView _canvasView;

        public HealthBarsPoolFactory(HealthBarsDataBase healthBarsDataBase, CanvasView canvasView)
        {
            _healthBarsDataBase = healthBarsDataBase;
            _canvasView = canvasView;
        }

        protected override EnemyHealthView CreateNewElement(HealthBarType key)
        {
            return key switch
            {
                HealthBarType.Red => Object.Instantiate(_healthBarsDataBase.HealthView, _canvasView.CanvasPoolRoot),
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, "Can't find health-bar.")
            };
        }
    }
}
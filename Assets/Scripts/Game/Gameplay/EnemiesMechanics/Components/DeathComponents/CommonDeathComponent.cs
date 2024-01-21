using System;
using Game.Gameplay.Views.Enemy;
using Zenject;

namespace Game.Gameplay.EnemiesMechanics.Components.DeathComponents
{
    public class CommonDeathComponent : IEnemyDeathComponent, IInitializable, IDisposable
    {
        private readonly EnemyViewBase _enemyViewBase;

        public event Action OnDied;

        public CommonDeathComponent(EnemyViewBase enemyViewBase)
        {
            _enemyViewBase = enemyViewBase;
        }

        public void BeginDie()
        {
            _enemyViewBase.BeginDie();
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyDeathCompleted += OnEnemyDeathCompleted;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyDeathCompleted -= OnEnemyDeathCompleted;
        }

        private void OnEnemyDeathCompleted()
        {
            OnDied?.Invoke();
        }
    }
}
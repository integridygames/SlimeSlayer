using System;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class CommonShootFxView : RecyclableParticleView
    {
        private Action<EnemyViewBase> _enemyCollideAction;

        public void SetEnemyCollideHandler(Action<EnemyViewBase> enemyCollideHandler)
        {
            _enemyCollideAction = enemyCollideHandler;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EnemyViewBase enemyView))
            {
                _enemyCollideAction?.Invoke(enemyView);
            }
        }
    }
}
using System;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class ProjectileView : RecyclableParticleView
    {
        private Action<EnemyView> _enemyCollideAction;

        public void SetEnemyCollideHandler(Action<EnemyView> enemyCollideHandler)
        {
            _enemyCollideAction = enemyCollideHandler;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EnemyView enemyView))
            {
                _enemyCollideAction?.Invoke(enemyView);
            }
        }
    }
}
using System;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class PushingImpulseView : RecyclableParticleView
    {
        public event Action<CommonEnemyView> OnEnemyCollide;

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out CommonEnemyView commonEnemyView))
            {
                OnEnemyCollide?.Invoke(commonEnemyView);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.Views.FX
{
    public class CommonShootFxView : RecyclableParticleView
    {
        public event Action<CommonShootFxView, EnemyViewBase, Vector3> OnEnemyCollide;

        private readonly List<ParticleCollisionEvent> _collisionEvents = new();

        public float Damage { get; set; }

        public void Initialize(float damage)
        {
            Damage = damage;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EnemyViewBase enemyView))
            {
                _particleSystem.GetCollisionEvents(other, _collisionEvents);

                foreach (var collisionEvent in _collisionEvents)
                {
                    OnEnemyCollide?.Invoke(this, enemyView, collisionEvent.intersection);
                }
            }
        }
    }
}
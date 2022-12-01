using System;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public event Action<HitInfo> OnEnemyHit;
        public event Action<Collision> OnEnemyCollide;

        private MeshRenderer _meshRenderer;

        public void InvokeHit(HitInfo hitInfo)
        {
            OnEnemyHit?.Invoke(hitInfo);
        }

        public void SetEssenceMaterial(Material material)
        {
            _meshRenderer ??= GetComponentInChildren<MeshRenderer>();

            _meshRenderer.material = material;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnEnemyCollide?.Invoke(collision);
        }
    }
}
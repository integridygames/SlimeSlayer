using System;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public event Action<Vector3, float> OnEnemyHit;

        private MeshRenderer _meshRenderer;

        public void InvokeHit(Vector3 hitPosition, float damage)
        {
            OnEnemyHit?.Invoke(hitPosition, damage);
        }

        public void SetEssenceMaterial(Material material)
        {
            _meshRenderer ??= GetComponentInChildren<MeshRenderer>();

            _meshRenderer.material = material;
        }
    }
}
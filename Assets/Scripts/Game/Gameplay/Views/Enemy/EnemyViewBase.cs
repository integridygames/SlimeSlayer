using System;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public event Action<HitInfo> OnEnemyHit;
        public event Action<Collision> OnEnemyCollide;

        private MeshFilter _meshFilter;
        public MeshFilter MeshFilter => _meshFilter ??= GetComponentInChildren<MeshFilter>();

        public void InvokeHit(HitInfo hitInfo)
        {
            OnEnemyHit?.Invoke(hitInfo);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnEnemyCollide?.Invoke(collision);
        }
    }
}
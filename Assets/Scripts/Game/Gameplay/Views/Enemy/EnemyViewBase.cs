using System;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public abstract event Action OnEnemyDeathCompleted;
        public abstract event Action OnEnemyAttack;
        public abstract event Action OnEnemyAttackCompleted;

        public event Action<HitInfo> OnEnemyHit;
        public event Action<Collision> OnEnemyCollide;

        private MeshFilter _meshFilter;
        public MeshFilter MeshFilter => _meshFilter ??= GetComponentInChildren<MeshFilter>();

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponentInChildren<Rigidbody>();

        public void InvokeHit(HitInfo hitInfo)
        {
            OnEnemyHit?.Invoke(hitInfo);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnEnemyCollide?.Invoke(collision);
        }

        public abstract void SetAttackAnimation(bool isAttack);

        public abstract void SetDeathAnimation();
    }
}
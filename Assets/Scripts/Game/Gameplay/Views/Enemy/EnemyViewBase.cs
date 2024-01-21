using System;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        public event Action OnEnemyDeathCompleted;
        public event Action OnEnemyAttack;
        public event Action OnEnemyAttackCompleted;

        public event Action<HitInfo> OnEnemyHit;
        public event Action<Collision> OnEnemyCollide;

        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent ??= GetComponentInChildren<NavMeshAgent>();

        private Collider _collider;
        public Collider Collider => _collider ??= GetComponentInChildren<Collider>();

        private Animator _animator;
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();

        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int Death = Animator.StringToHash("Death");

        public void InvokeHit(HitInfo hitInfo)
        {
            OnEnemyHit?.Invoke(hitInfo);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnEnemyCollide?.Invoke(collision);
        }

        public void BeginDie()
        {
            NavMeshAgent.enabled = false;

            Animator.applyRootMotion = true;
            Collider.enabled = false;

            Animator.SetTrigger(Death);
        }

        public void SetAttackAnimation(bool isAttack)
        {
            Animator.SetBool(IsAttack, isAttack);
        }

        public void OnDeathAnimationCompleted()
        {
            OnEnemyDeathCompleted?.Invoke();
        }

        public void OnMidOfAttackAnimation()
        {
            OnEnemyAttack?.Invoke();
        }

        public void OnEndOfAttackAnimation()
        {
            OnEnemyAttackCompleted?.Invoke();
        }
    }
}
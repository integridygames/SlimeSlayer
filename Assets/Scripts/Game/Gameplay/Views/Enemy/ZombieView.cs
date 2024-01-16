using System;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class ZombieView : EnemyViewBase
    {
        [SerializeField] private Collider _zombieCollider;
        [SerializeField] private Rigidbody _zombieRigidbody;

        private Animator _animator;
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();

        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int Death = Animator.StringToHash("Death");

        public override event Action OnEnemyDeathCompleted;
        public override event Action OnEnemyAttack;
        public override event Action OnEnemyAttackCompleted;

        public override void SetAttackAnimation(bool isAttack)
        {
            Animator.SetBool(IsAttack, isAttack);
        }

        public override void SetDeathAnimation()
        {
            Animator.applyRootMotion = true;
            _zombieCollider.enabled = false;
            _zombieRigidbody.isKinematic = true;
            _zombieRigidbody.constraints = RigidbodyConstraints.None;

            Animator.SetTrigger(Death);
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
using System;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class ZombieView : EnemyViewBase
    {
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
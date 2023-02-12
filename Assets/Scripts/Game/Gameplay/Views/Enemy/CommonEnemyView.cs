using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class CommonEnemyView : EnemyViewBase
    {
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponentInChildren<Rigidbody>();
    }
}
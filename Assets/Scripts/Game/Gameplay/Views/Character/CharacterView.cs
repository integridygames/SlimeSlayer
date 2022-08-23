using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character
{
    public class CharacterView : ViewBase
    {
        private Animator _animator;
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();
    }
}
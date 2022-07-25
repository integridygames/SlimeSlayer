using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Views.Character 
{
    public class CharacterView : ViewBase, IInitializable
    {
        [SerializeField] private Transform _leftHandBone;
        [SerializeField] private Transform _rightHandBone;

        private Animator _animator;

        public Animator Animator => _animator;
        public Transform LeftHandBone => _leftHandBone;
        public Transform RightHandBone => _rightHandBone;

        public void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}
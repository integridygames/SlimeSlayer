using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Views.Character 
{
    public class CharacterView : ViewBase, IInitializable
    {      
        private Animator _animator;

        public Animator Animator => _animator;     

        public void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}
using TegridyCore.Base;
using UnityEngine;
using Zenject;
using Game.Gameplay.Views.Character.Placers;

namespace Game.Gameplay.Views.Character 
{
    public class CharacterView : ViewBase, IInitializable
    {
        [SerializeField] private WeaponPlacer _leftWeaponPosition;
        [SerializeField] private WeaponPlacer _rightWeaponPosition;

        private Animator _animator;
        public WeaponPlacer LeftWeaponPosition => _leftWeaponPosition;
        public WeaponPlacer RightWeaponPosition => _rightWeaponPosition;

        public Animator Animator => _animator;     

        public void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}
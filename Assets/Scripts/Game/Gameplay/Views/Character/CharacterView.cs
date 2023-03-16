using Game.Gameplay.Views.Character.Placers;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character
{
    public class CharacterView : ViewBase
    {
        [SerializeField] private WeaponPlacer _leftWeaponPlacer;
        [SerializeField] private WeaponPlacer _rightWeaponPlacer;

        private Animator _animator;
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();

        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public WeaponPlacer LeftWeaponPlacer => _leftWeaponPlacer;
        public WeaponPlacer RightWeaponPlacer => _rightWeaponPlacer;
    }
}
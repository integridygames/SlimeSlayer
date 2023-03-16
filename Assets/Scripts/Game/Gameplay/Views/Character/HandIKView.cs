using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Character.Targets;

namespace Game.Gameplay.Views.Character
{
    public class HandIKView : ViewBase
    {
        [SerializeField] private HandTargetView _handTargetView;
        [SerializeField] private HandRotationCenterView _handRotationCenter;
        [SerializeField] private AvatarIKGoal _avatarIKGoal;

        private Animator _animator;
        private float _weight;

        public HandTargetView HandTargetView => _handTargetView;
        public HandRotationCenterView HandRotationCenterView => _handRotationCenter;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetWeight(float weight)
        {
            _weight = weight;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetIKPositionWeight(_avatarIKGoal, _weight);
            _animator.SetIKRotationWeight(_avatarIKGoal, _weight);

            _animator.SetIKPosition(_avatarIKGoal, _handTargetView.transform.position);
            _animator.SetIKRotation(_avatarIKGoal, _handRotationCenter.transform.rotation);
        }
    }
}
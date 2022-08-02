using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Character.Targets;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character 
{
    public class HandTargetsSetterSystem : IInitializeSystem
    {
        private HandTargetView _leftHandTargetView;
        private HandTargetView _rightHandTargetView;
        
        private const float _leftPositionX = 0.05319402f;
        private const float _leftPositionY = 0.008044004f;
        private const float _leftPositionZ = 0.7770309f;

        private const float _rightPositionX = -0.02426209f;
        private const float _rightPositionY = 0.008044004f;
        private const float _rightPositionZ = 0.7970156f;
      
        private const float _leftRotationX = -68f;
        private const float _leftRotationY = -48.6f;
        private const float _leftRotationZ = 54.2f;

        private const float _rightRotationX = -68f;
        private const float _rightRotationY = 48.6f;
        private const float _rightRotationZ = -54.2f;

        public HandTargetsSetterSystem(List<HandIKView> handIKViews) 
        {
            foreach(var handIKView in handIKViews) 
            {
                if (handIKView.HandTargetView.IsLeft)
                    _leftHandTargetView = handIKView.HandTargetView;
                else
                    _rightHandTargetView = handIKView.HandTargetView;
            }
        }

        public void Initialize()
        {
            _leftHandTargetView.transform.localPosition = new Vector3(_leftPositionX, _leftPositionY, _leftPositionZ);
            _rightHandTargetView.transform.localPosition = new Vector3(_rightPositionX, _rightPositionY, _rightPositionZ);
            _leftHandTargetView.transform.localRotation = Quaternion.Euler(_leftRotationX, _leftRotationY, _leftRotationZ);
            _rightHandTargetView.transform.localRotation = Quaternion.Euler(_rightRotationX, _rightRotationY, _rightRotationZ);
        }
    }
}
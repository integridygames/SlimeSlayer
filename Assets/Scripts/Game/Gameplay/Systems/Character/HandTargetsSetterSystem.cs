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
        
        private const float LeftPositionX = 0.05319402f;
        private const float LeftPositionY = 0.008044004f;
        private const float LeftPositionZ = 0.7770309f;

        private const float RightPositionX = -0.02426209f;
        private const float RightPositionY = 0.008044004f;
        private const float RightPositionZ = 0.7970156f;
      
        private const float LeftRotationX = -68f;
        private const float LeftRotationY = -48.6f;
        private const float LeftRotationZ = 54.2f;

        private const float RightRotationX = -68f;
        private const float RightRotationY = 48.6f;
        private const float RightRotationZ = -54.2f;

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
            _leftHandTargetView.transform.localPosition = new Vector3(LeftPositionX, LeftPositionY, LeftPositionZ);
            _rightHandTargetView.transform.localPosition = new Vector3(RightPositionX, RightPositionY, RightPositionZ);
            _leftHandTargetView.transform.localRotation = Quaternion.Euler(LeftRotationX, LeftRotationY, LeftRotationZ);
            _rightHandTargetView.transform.localRotation = Quaternion.Euler(RightRotationX, RightRotationY, RightRotationZ);
        }
    }
}
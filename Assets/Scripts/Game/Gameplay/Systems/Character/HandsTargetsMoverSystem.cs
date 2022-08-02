using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Character.Targets;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character 
{
    public class HandsTargetsMoverSystem : IFixedUpdateSystem
    {
        private HandTargetView _leftHandTargetView;
        private HandTargetView _rightHandTargetView;
        private HandRotationCenterView _leftRotationCenter;
        private HandRotationCenterView _rightRotationCenter;
        private CharacterView _characterView;
        private RaycastInfo _raycastInfo;
        private Quaternion _rotationToZero;

        private const float _maxAngle = 80;
        private const float _rotationSpeed = 100;

        public HandsTargetsMoverSystem(List<HandIKView> handIKViews, CharacterView characterView, RaycastInfo raycastInfo)
        {
            foreach (var handIKView in handIKViews)
            {
                if (handIKView.HandTargetView.IsLeft)
                    _leftHandTargetView = handIKView.HandTargetView;
                else
                    _rightHandTargetView = handIKView.HandTargetView;

                if (handIKView.HandRotationCenterView.IsLeft)
                    _leftRotationCenter = handIKView.HandRotationCenterView;
                else
                    _rightRotationCenter = handIKView.HandRotationCenterView;
            }           

            _characterView = characterView;
            _raycastInfo = raycastInfo;
            _rotationToZero = Quaternion.Euler(0, 0, 0);
        }

        public void FixedUpdate()
        {
            Collider[] targets = Physics.OverlapSphere(_characterView.transform.position, _raycastInfo.Radius, _raycastInfo.LayerNumber);
            List<Collider> filteredTargets = FilterTargets(targets);

            FindTwoNearestTargets(filteredTargets, out Vector3 nearestTarget, out Vector3 secondNearestTarget,
            out bool nearestTargetExists, out bool secondNearestTargetExists);              

            switch (nearestTargetExists, secondNearestTargetExists) 
            {
                case (true, false):                  
                    _leftRotationCenter.transform.rotation = Quaternion.RotateTowards(_leftRotationCenter.transform.rotation, 
                        SetTargetRotation(nearestTarget, _leftRotationCenter.transform.position), _rotationSpeed * Time.fixedDeltaTime);
                    _rightRotationCenter.transform.rotation = Quaternion.RotateTowards(_rightRotationCenter.transform.rotation,
                        SetTargetRotation(nearestTarget, _rightRotationCenter.transform.position), _rotationSpeed * Time.fixedDeltaTime);
                    break;
                case (true, true):
                    DetermineSide(nearestTarget, secondNearestTarget, _leftRotationCenter.transform.position, _rightRotationCenter.transform.position,
                         out Vector3 nearestForLeft, out Vector3 nearestForRight);

                    _leftRotationCenter.transform.rotation = Quaternion.RotateTowards(_leftRotationCenter.transform.rotation,
                        SetTargetRotation(nearestForLeft, _leftRotationCenter.transform.position), _rotationSpeed * Time.fixedDeltaTime);
                    _rightRotationCenter.transform.rotation = Quaternion.RotateTowards(_rightRotationCenter.transform.rotation,
                        SetTargetRotation(nearestForRight, _rightRotationCenter.transform.position), _rotationSpeed * Time.fixedDeltaTime);
                    break;
                case (false, false):
                    _leftRotationCenter.transform.rotation = Quaternion.RotateTowards(_leftRotationCenter.transform.rotation,
                        _rotationToZero, _rotationSpeed * Time.fixedDeltaTime);
                    _rightRotationCenter.transform.rotation = Quaternion.RotateTowards(_rightRotationCenter.transform.rotation,
                        _rotationToZero, _rotationSpeed * Time.fixedDeltaTime);
                    break;
            }
        }

        private List<Collider> FilterTargets(Collider[] targets) 
        {
            List<Collider> filteredTargets = new List<Collider>();
            foreach(var target in targets) 
            {
                Vector3 characterDirectionVector = new Vector3(_characterView.transform.forward.x, 0, _characterView.transform.forward.z);
                Vector3 characterVector = new Vector3(_characterView.transform.position.x, 0, _characterView.transform.position.z);
                Vector3 targetToCharacterVector = new Vector3(target.transform.position.x, 0, target.transform.position.z) - characterVector;
                if(Vector3.Angle(characterDirectionVector, targetToCharacterVector) <= _maxAngle) 
                {
                    filteredTargets.Add(target);
                }
            }

            return filteredTargets;
        }

        private void FindTwoNearestTargets(List<Collider> targets, out Vector3 nearestTarget, out Vector3 secondNearestTarget, 
            out bool nearestTargetExists, out bool secondNearestTargetExists) 
        {
            float minDistance = float.MaxValue;
            float secondMinDistance = float.MaxValue;
            nearestTargetExists = false;
            secondNearestTargetExists = false;

            nearestTarget = Vector3.zero;
            secondNearestTarget = Vector3.zero;
            int targetsQuantityMinusOne = 0;
            if (targets.Count > 0) 
            {
                targetsQuantityMinusOne = targets.Count - 1;
                minDistance = Vector3.Distance(_characterView.transform.position, targets[0].transform.position);
                nearestTarget = targets[0].transform.position;
                nearestTargetExists = true;
            }

            for (int i = 0; i < targetsQuantityMinusOne; i++) 
            {
                float distance = Vector3.Distance(_characterView.transform.position, targets[i + 1].transform.position);
                if (distance < minDistance) 
                {
                    secondMinDistance = minDistance;
                    minDistance = distance;
                    secondNearestTarget = nearestTarget;
                    nearestTarget = targets[i + 1].transform.position;
                    secondNearestTargetExists = true;
                }
                else if(distance < secondMinDistance) 
                {
                    secondMinDistance = distance;
                    secondNearestTarget = targets[i + 1].transform.position;
                    secondNearestTargetExists = true;
                }
            }
        }

        private Quaternion SetTargetRotation(Vector3 nearestTarget, Vector3 rotationCenter) 
        {
            Vector3 directionToTarget = nearestTarget - rotationCenter;
            return Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        }

        private void DetermineSide(Vector3 nearestTarget, Vector3 secondNearestTarget, Vector3 leftRotationCenter, Vector3 rightRotationCenter,
            out Vector3 nearestForLeft, out Vector3 nearestForRight) 
        {
            float leftDistanceToNearest = Vector3.Distance(nearestTarget, leftRotationCenter);
            float rightDistanceToNearest = Vector3.Distance(nearestTarget, rightRotationCenter);

            if(leftDistanceToNearest < rightDistanceToNearest) 
            {
                nearestForLeft = nearestTarget;
                nearestForRight = secondNearestTarget;
            }
            else 
            {
                nearestForLeft = secondNearestTarget;
                nearestForRight = nearestTarget;
            }
        }
    }
}
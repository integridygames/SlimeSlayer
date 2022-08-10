using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Character.Targets;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class HandsTargetsMoverSystem : IFixedUpdateSystem
    {
        private readonly HandRotationCenterView _leftRotationCenter;
        private readonly HandRotationCenterView _rightRotationCenter;
        private readonly CharacterView _characterView;
        private readonly Quaternion _rotationToZero;
        private readonly TargetsInfo _targetsInfo;

        private const float MaxAngle = 80;
        private const float RotationSpeed = 100;

        public HandsTargetsMoverSystem(List<HandIKView> handIKViews, CharacterView characterView, TargetsInfo targetsInfo, 
            CharacterHandsMovingStats statsInfo)
        {
            foreach (var handIKView in handIKViews)
            {              
                if (handIKView.HandRotationCenterView.IsLeft)
                    _leftRotationCenter = handIKView.HandRotationCenterView;
                else
                    _rightRotationCenter = handIKView.HandRotationCenterView;
            }           

            _characterView = characterView;
            _rotationToZero = Quaternion.Euler(0, 0, 0);
            _targetsInfo = targetsInfo;
        }

        public void FixedUpdate()
        {
            Collider[] targets = _targetsInfo.Targets;
            List<Collider> filteredTargets = FilterTargets(targets);

            FindTwoNearestTargets(filteredTargets, out Vector3 nearestTarget, out Vector3 secondNearestTarget,
            out bool nearestTargetExists, out bool secondNearestTargetExists);              

            switch (nearestTargetExists, secondNearestTargetExists) 
            {
                case (true, false):
                    RotateArmsToOneSide(Time.fixedDeltaTime, nearestTarget);
                    break;
                case (true, true):
                    RotateArmsToDifferentSides(Time.fixedDeltaTime, nearestTarget, secondNearestTarget);                  
                    break;
                case (false, false):
                    RotateArmsToDefaultRotation(Time.fixedDeltaTime);
                    break;
            }
        }

        private void RotateArmsToOneSide(float fixedDeltaTime, Vector3 nearestTarget) 
        {
            _leftRotationCenter.transform.rotation = Quaternion.RotateTowards(_leftRotationCenter.transform.rotation,
                SetTargetRotation(nearestTarget, _leftRotationCenter.transform.position), RotationSpeed * fixedDeltaTime);
            _rightRotationCenter.transform.rotation = Quaternion.RotateTowards(_rightRotationCenter.transform.rotation,
                SetTargetRotation(nearestTarget, _rightRotationCenter.transform.position), RotationSpeed * fixedDeltaTime);
        }

        private void RotateArmsToDifferentSides(float fixedDeltaTime, Vector3 nearestTarget, Vector3 secondNearestTarget) 
        {
            DetermineSide(nearestTarget, secondNearestTarget, _leftRotationCenter.transform.position, _rightRotationCenter.transform.position,
                        out Vector3 nearestForLeft, out Vector3 nearestForRight);

            _leftRotationCenter.transform.rotation = Quaternion.RotateTowards(_leftRotationCenter.transform.rotation,
                SetTargetRotation(nearestForLeft, _leftRotationCenter.transform.position), RotationSpeed * fixedDeltaTime);
            _rightRotationCenter.transform.rotation = Quaternion.RotateTowards(_rightRotationCenter.transform.rotation,
                SetTargetRotation(nearestForRight, _rightRotationCenter.transform.position), RotationSpeed * fixedDeltaTime);
        }

        private void RotateArmsToDefaultRotation(float fixedDeltaTime) 
        {
            _leftRotationCenter.transform.localRotation = Quaternion.RotateTowards(_leftRotationCenter.transform.localRotation,
                       _rotationToZero, RotationSpeed * fixedDeltaTime);
            _rightRotationCenter.transform.localRotation = Quaternion.RotateTowards(_rightRotationCenter.transform.localRotation,
                _rotationToZero, RotationSpeed * fixedDeltaTime);
        }

        private List<Collider> FilterTargets(Collider[] targets) 
        {
            List<Collider> filteredTargets = new List<Collider>();
            foreach(var target in targets) 
            {
                if(target != null) 
                {
                    Vector3 characterDirectionVector = new Vector3(_characterView.transform.forward.x, 0, _characterView.transform.forward.z);
                    Vector3 characterVector = new Vector3(_characterView.transform.position.x, 0, _characterView.transform.position.z);
                    Vector3 targetToCharacterVector = new Vector3(target.transform.position.x, 0, target.transform.position.z) - characterVector;
                    if (Vector3.Angle(characterDirectionVector, targetToCharacterVector) <= MaxAngle)
                    {
                        filteredTargets.Add(target);
                    }
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
            nearestForLeft = Vector3.zero;
            nearestForRight = Vector3.zero;

            DetermineDirections(nearestTarget, secondNearestTarget, out float directionNearest, out float directionSecondNearest);
            NormalizeDirections(directionNearest, directionSecondNearest, out float directionNearestNormalized, out float directionSecondNearestNormalized);
          
            switch ((directionNearestNormalized, directionSecondNearestNormalized)) 
            {
                case (0, 0):
                    SetTargetsIfBothZero(nearestTarget, secondNearestTarget, out nearestForLeft, out nearestForRight);                
                    break;
                case (-1, -1):
                    SetTargetsIfEqual(directionNearest, directionSecondNearest, nearestTarget, secondNearestTarget, out nearestForLeft, out nearestForRight);
                    break;
                case (1, 1):
                    SetTargetsIfEqual(directionNearest, directionSecondNearest, nearestTarget, secondNearestTarget, out nearestForLeft, out nearestForRight);
                    break;
                case (-1, 1):
                    SetTargetsIfNotEqual(nearestTarget, secondNearestTarget, out nearestForLeft, out nearestForRight);
                    break;
                case (1, -1):
                    SetTargetsIfNotEqual(secondNearestTarget, nearestTarget, out nearestForLeft, out nearestForRight);
                    break;
            }
        }

        private void SetTargetsIfBothZero(Vector3 nearestTarget, Vector3 secondNearestTarget, out Vector3 nearestForLeft, out Vector3 nearestForRight) 
        {
            if (Vector3.Distance(_characterView.transform.position, nearestTarget) < Vector3.Distance(_characterView.transform.position, secondNearestTarget))
            {
                nearestForLeft = nearestTarget;
                nearestForRight = nearestTarget;
            }
            else
            {
                nearestForLeft = secondNearestTarget;
                nearestForRight = secondNearestTarget;
            }
        }

        private void SetTargetsIfEqual(float directionNearest, float directionSecondNearest, Vector3 nearestTarget, Vector3 secondNearestTarget,
            out Vector3 nearestForLeft, out Vector3 nearestForRight) 
        {
            if (directionNearest < directionSecondNearest)
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

        private void SetTargetsIfNotEqual(Vector3 firstNearest, Vector3 secondNearest, out Vector3 nearestForLeft, out Vector3 nearestForRight) 
        {
            nearestForLeft = firstNearest;
            nearestForRight = secondNearest;
        }

        private void NormalizeDirections(float directionNearest, float directionSecondNearest, out float directionNearestNormalized, out float directionSecondNearestNormalized) 
        {
            directionNearestNormalized = directionNearest / Mathf.Abs(directionNearest);
            directionSecondNearestNormalized = directionSecondNearest / Mathf.Abs(directionSecondNearest);
        }

        private void DetermineDirections(Vector3 nearestTarget, Vector3 secondNearestTarget, out float directionNearest, out float directionSecondNearest) 
        {
            directionNearest = DetermineDirection(nearestTarget);
            directionSecondNearest = DetermineDirection(secondNearestTarget);
        }

        private float DetermineDirection(Vector3 target) 
        {
            return (target.x - _characterView.transform.position.x) *
                (_characterView.transform.forward.z - _characterView.transform.position.z) -
                (target.z - _characterView.transform.position.z) *
                (_characterView.transform.forward.x - _characterView.transform.position.x);
        }
    }
}
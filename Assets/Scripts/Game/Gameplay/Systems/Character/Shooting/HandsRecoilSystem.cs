using System.Collections.Generic;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Shooting
{
    public class HandsRecoilSystem : IUpdateSystem
    {
        private const float RecoilAmplitude = 0.5f;
        private const float RecoilSpeed = 20f;
        private const float RecoilTime = 0.05f;

        private readonly List<HandIKView> _handIKViews;
        private readonly CharacterView _characterView;

        public HandsRecoilSystem(List<HandIKView> handIKViews, CharacterView characterView)
        {
            _handIKViews = handIKViews;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach (var handIKView in _handIKViews)
            {
                if (handIKView.IsOnRecoil)
                {
                    var transform = handIKView.HandTargetView.transform;
                    var recoilVector =handIKView.HandTargetView.transform.forward * -RecoilAmplitude;
                    recoilVector = handIKView.HandTargetView.transform.InverseTransformDirection(recoilVector);

                    handIKView.HandTargetView.transform.localPosition = Vector3.Lerp(transform.localPosition, handIKView.HandTargetView.StartLocalPosition + recoilVector, Time.deltaTime * RecoilSpeed);

                    handIKView.RecoilProgress -= Time.deltaTime / RecoilTime;

                    if (handIKView.RecoilProgress <= 0f)
                        handIKView.IsOnRecoil = false;

                    continue;
                }

                handIKView.HandTargetView.transform.localPosition = Vector3.Lerp(handIKView.HandTargetView.transform.localPosition, handIKView.HandTargetView.StartLocalPosition, Time.deltaTime * RecoilSpeed);
            }
        }
    }
}
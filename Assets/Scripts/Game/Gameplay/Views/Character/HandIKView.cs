using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Views.Character.Bones;
using Game.Gameplay.Views.Character.Poles;
using Game.Gameplay.Views.Character.Targets;

namespace Game.Gameplay.Views.Character
{
    public class HandIKView : ViewBase
    {
        [SerializeField] private HandBoneView _handBoneView;
        [SerializeField] private HandTargetView _handTargetView;
        [SerializeField] private HandPoleView _handPoleView;

        public HandBoneView HandBoneView => _handBoneView;
        public HandTargetView HandTargetView => _handTargetView;
        public HandPoleView HandPoleView => _handPoleView;
    }
}
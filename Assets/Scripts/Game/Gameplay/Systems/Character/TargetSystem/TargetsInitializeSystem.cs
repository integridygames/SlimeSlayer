using System;
using Game.Gameplay.Models.Character.TargetSystem;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem 
{
    public class TargetsInitializeSystem : IInitializeSystem
    {
        private readonly TargetsInfo _targetsInfo;

        public TargetsInitializeSystem(TargetsInfo targetsInfo)
        {
            _targetsInfo = targetsInfo;
        }

        public void Initialize()
        {
            _targetsInfo.Targets = Array.Empty<Collider>();
        }
    }
}
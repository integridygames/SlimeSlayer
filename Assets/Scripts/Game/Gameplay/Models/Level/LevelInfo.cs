﻿using Game.Gameplay.Views.Level;
using JetBrains.Annotations;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.Models.Level
{
    [UsedImplicitly]
    public class LevelInfo
    {
        public RxField<LevelView> CurrentLevelView { get; set; } = new();
        public RxField<LevelView> NextLevelView { get; set; } = new();
        public bool DoesNextLevelExist { get; set; }
        public Vector3 DistanceBetweenNearestLevels { get; private set; } = new Vector3(0, 0, 300);
        public Vector3 ActualNextLevelPosition { get; set; } = new Vector3(0, 0, 300);
    }
}
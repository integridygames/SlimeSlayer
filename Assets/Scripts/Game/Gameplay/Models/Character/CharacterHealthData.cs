﻿using TegridyCore;

namespace Game.Gameplay.Models.Character
{
    public class CharacterHealthData
    {
        public RxField<int> CurrentHealth { get; } = 0;

        public int MaxHealth { get; set; }
    }
}
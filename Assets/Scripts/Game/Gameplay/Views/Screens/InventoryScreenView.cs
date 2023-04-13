﻿using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.Screens
{
    public class InventoryScreenView : ViewBase
    {
        [SerializeField] private UiButton _closeButton;

        public UiButton CloseButton => _closeButton;
    }
}
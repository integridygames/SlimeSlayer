using System;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.SampleScene.Screens
{
    public class GameScreenView : ViewBase
    {
        [SerializeField] private UiButton _toStartScreenButton;

        public event Action OnReleased;

        public UiButton ToStartScreenButton => _toStartScreenButton;
    }
}

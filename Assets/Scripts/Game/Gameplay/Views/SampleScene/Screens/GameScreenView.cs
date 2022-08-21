using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.SampleScene.Screens
{
    public class GameScreenView : ViewBase
    {
        [SerializeField] private UiButton _toStartScreenButton;

        public UiButton ToStartScreenButton => _toStartScreenButton;
    }
}

using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.SampleScene.Screens
{
    public class StartScreenView : ViewBase
    {
        [SerializeField] private UiButton _startGameButton;

        public UiButton StartGameButton => _startGameButton;
    }
}

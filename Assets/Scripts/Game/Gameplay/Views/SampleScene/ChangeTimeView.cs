using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.SampleScene
{
    public class ChangeTimeView : ViewBase
    {
        [SerializeField] private UiButton _changeTimeButton;

        public UiButton ChangeTimeButton => _changeTimeButton;
    }
}
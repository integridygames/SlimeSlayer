using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTransitMenuView : ViewBase
    {
        [SerializeField] private EssenceImageView[] _essenceImageViews;
        [SerializeField] private Button _openButton;

        public EssenceImageView[] EssenceImageViews => _essenceImageViews;
        public Button OpenButton => _openButton;
    }
}
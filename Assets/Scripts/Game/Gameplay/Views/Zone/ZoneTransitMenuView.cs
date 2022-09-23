using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using System.Collections.Generic;
using System.Linq;
using TegridyCore.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTransitMenuView : ViewBase
    {
        [SerializeField] private EssenceImageView[] _essenceImageViews;
        [SerializeField] private Button _openButton;

        public Dictionary<EssenceType, EssenceImageView> EssenceImageViewsDictionary { get; private set; }
        public List<EssenceImageView> EssenceImageViewsList { get; private set; }
        public Button OpenButton => _openButton;

        public void Initialzie() 
        {
            EssenceImageViewsDictionary = _essenceImageViews.ToDictionary(key => key.EssenceType);
            EssenceImageViewsList = _essenceImageViews.ToList();
        }
    }
}
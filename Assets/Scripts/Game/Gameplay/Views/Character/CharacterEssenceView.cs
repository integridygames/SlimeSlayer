using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character 
{
    public class CharacterEssenceView : ViewBase
    {
        [SerializeField] private EssenceImageView[] _essenceImageViews;

        public EssenceImageView[] EssenceImageViews => _essenceImageViews;
    }
}
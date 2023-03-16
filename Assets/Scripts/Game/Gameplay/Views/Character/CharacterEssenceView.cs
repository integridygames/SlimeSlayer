using System.Collections.Generic;
using System.Linq;
using Game.DataBase;
using Game.DataBase.Essence;
using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character
{
    public class CharacterEssenceView : ViewBase
    {
        [SerializeField] private EssenceImageView[] _essenceImageViews;

        private Dictionary<EssenceType, EssenceImageView> _essenceImageViewsByType;

        public Dictionary<EssenceType, EssenceImageView> EssenceImageViewsByType =>
            _essenceImageViewsByType ??= _essenceImageViews.ToDictionary(x => x.EssenceType);
    }
}
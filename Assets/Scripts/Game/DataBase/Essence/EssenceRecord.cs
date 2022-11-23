using System;
using Game.Gameplay.Views.Essence;
using UnityEngine;

namespace Game.DataBase.Essence
{
    [Serializable]
    public class EssenceRecord
    {
        public EssenceType _essenceType;
        public EssenceView _essenceView;
        public Material _material;
    }
}
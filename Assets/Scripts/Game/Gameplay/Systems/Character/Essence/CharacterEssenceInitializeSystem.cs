using Game.Gameplay.Models.Character;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Essence 
{
    public class CharacterEssenceInitializeSystem : IInitializeSystem
    {
        private readonly CharacterEssencesData _characterEssencesInfo;
        private readonly EssenceDataBase _essenceDataBase;
        private readonly CharacterEssenceView _characterEssenceView;

        public CharacterEssenceInitializeSystem(CharacterEssencesData characterEssencesInfo, EssenceDataBase essenceDataBase, 
            CharacterEssenceView characterEssenceView) 
        {
            _characterEssencesInfo = characterEssencesInfo;
            _essenceDataBase = essenceDataBase;
            _characterEssenceView = characterEssenceView;

        }

        public void Initialize()
        {
            InitializeCharacterEssenceInfo();
        } 

        private void InitializeCharacterEssenceInfo() 
        {
            _characterEssencesInfo.CharacterEssences = new();
            foreach (var essenceRecord in _essenceDataBase.Records)
            {
                if(FindEssenceImageView(essenceRecord._recordType, out var essenceImageView)) 
                {
                    _characterEssencesInfo.CharacterEssences.Add(new (essenceRecord._recordType, essenceImageView,  0));
                    essenceImageView.QuantityTMPText.text = "0";
                    essenceImageView.EssenceImage.color = FindColor(essenceRecord._recordType);
                }
            }
        }

        private bool FindEssenceImageView(EssenceType essenceType, out EssenceImageView retturnedEssenceImageView) 
        {
            foreach(var essenceImageView in _characterEssenceView.EssenceImageViews) 
            {
                if (essenceImageView.EssenceType == essenceType) 
                {
                    retturnedEssenceImageView = essenceImageView;
                    return true;
                }
            }

            retturnedEssenceImageView = null;
            return false;
        }     

        private Color FindColor(EssenceType essenceType) 
        {
            foreach(var record in _essenceDataBase.Records) 
            {
                if(record._recordType == essenceType) 
                {
                    return record.EssenceColor;
                }
            }

            return Color.black;
        }
    }   
}
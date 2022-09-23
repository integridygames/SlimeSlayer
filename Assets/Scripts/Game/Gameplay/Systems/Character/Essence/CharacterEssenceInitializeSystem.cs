using Game.Gameplay.Models.Character;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character.Essence 
{
    public class CharacterEssenceInitializeSystem : IInitializeSystem
    {
        private readonly CharacterEssencesData _characterEssencesData;
        private readonly EssenceDataBase _essenceDataBase;
        private readonly CharacterEssenceView _characterEssenceView;

        public CharacterEssenceInitializeSystem(CharacterEssencesData characterEssencesData, EssenceDataBase essenceDataBase, 
            CharacterEssenceView characterEssenceView) 
        {
            _characterEssencesData = characterEssencesData;
            _essenceDataBase = essenceDataBase;
            _characterEssenceView = characterEssenceView;
        }

        public void Initialize()
        {
            InitializeCharacterEssenceData();
        } 

        private void InitializeCharacterEssenceData() 
        {
            _characterEssencesData.Initialize();


            foreach (var essenceRecord in _essenceDataBase.Records)
            {
                if(FindEssenceImageView(essenceRecord._recordType, out var essenceImageView)) 
                {
                    _characterEssencesData.CharacterEssences.Add(essenceRecord._recordType, new(essenceImageView,  0));
                    essenceImageView.QuantityTMPText.text = "0";
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
    }   
}
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using Game.ScriptableObjects;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character.Essence
{
    public class CharacterEssenceInitializeSystem : IInitializeSystem
    {
        private readonly CharacterEssencesData _characterEssencesData;
        private readonly EssenceDataBase _essenceDataBase;
        private readonly CharacterEssenceView _characterEssenceView;

        public CharacterEssenceInitializeSystem(CharacterEssencesData characterEssencesData,
            EssenceDataBase essenceDataBase,
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
                var essenceType = essenceRecord._essenceType;

                if (_characterEssenceView.EssenceImageViewsByType.TryGetValue(essenceType, out var essenceImageView))
                {
                    _characterEssencesData.CharacterEssences.Add(essenceType, new CharacterEssence(essenceImageView, 0));
                }
            }
        }
    }
}
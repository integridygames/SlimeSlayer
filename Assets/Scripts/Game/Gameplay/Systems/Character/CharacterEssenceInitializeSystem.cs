using Game.Gameplay.Models.Character;
using Game.ScriptableObjects;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character 
{
    public class CharacterEssenceInitializeSystem : IInitializeSystem
    {
        private readonly CharacterEssencesData _characterEssencesInfo;
        private readonly EssenceDataBase _essenceDataBase;

        public CharacterEssenceInitializeSystem(CharacterEssencesData characterEssencesInfo, EssenceDataBase essenceDataBase) 
        {
            _characterEssencesInfo = characterEssencesInfo;
            _essenceDataBase = essenceDataBase;
        }

        public void Initialize()
        {
            _characterEssencesInfo.CharacterEssences = new();
            foreach (var essenceRecord in _essenceDataBase.Essences) 
            {
                _characterEssencesInfo.CharacterEssences.Add(essenceRecord.EssenceType, 0);
            }
        } 
    }   
}
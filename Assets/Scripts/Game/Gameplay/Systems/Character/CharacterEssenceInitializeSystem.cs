using Game.Gameplay.Models.Character;
using Game.Gameplay.Utils.Essences;
using Game.ScriptableObjects;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character 
{
    public class CharacterEssenceInitializeSystem : IInitializeSystem
    {
        private readonly CharacterEssencesInfo _characterEssencesInfo;
        private readonly EssenceDataBase _essenceDataBase;

        public CharacterEssenceInitializeSystem(CharacterEssencesInfo characterEssencesInfo, EssenceDataBase essenceDataBase) 
        {
            _characterEssencesInfo = characterEssencesInfo;
            _essenceDataBase = essenceDataBase;
        }

        public void Initialize()
        {
            _characterEssencesInfo.CharacterEssences = new();
            foreach (var essenceRecord in _essenceDataBase.Essences) 
            {
                _characterEssencesInfo.CharacterEssences.Add(new Essence(essenceRecord.EssenceType, 0));
            }
        } 
    }   
}
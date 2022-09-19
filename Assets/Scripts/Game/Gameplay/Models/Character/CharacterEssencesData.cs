using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssencesData
    {
        public List<CharacterEssence> CharacterEssences { get; set; }

        public bool FindEssence(EssenceType essenceType, out CharacterEssence retturnedCharacterEssence) 
        {
            foreach (var characterEssence in CharacterEssences) 
            {
                if (characterEssence.EssenceType == essenceType) 
                {
                    retturnedCharacterEssence = characterEssence;
                    return true;
                }                 
            }

            retturnedCharacterEssence = null;
            return false;
        }    
    }   
}
using Game.Gameplay.Utils.Essences;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssencesData
    {
        public List<CharacterEssence> CharacterEssences { get; set; }

        public bool FindEssence(EssenceType essenceType, out CharacterEssence returnedCharacterEssence) 
        {
            foreach (var characterEssence in CharacterEssences) 
            {
                if (characterEssence.EssenceType == essenceType) 
                {
                    returnedCharacterEssence = characterEssence;
                    return true;
                }                 
            }

            returnedCharacterEssence = null;
            return false;
        }    
    }   
}
using Game.Gameplay.Utils.Essences;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssencesData
    {
        public Dictionary<EssenceType, CharacterEssence> CharacterEssences { get; private set; }

        public void Initialize() 
        {
            CharacterEssences = new();
        }     
    }   
}
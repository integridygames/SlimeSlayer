using Game.Gameplay.Models.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Essence
{
    public class CharacterEssenceDestroySystem : IDestroySystem
    {
        private readonly CharacterEssencesData _characterEssencesData;

        public CharacterEssenceDestroySystem(CharacterEssencesData characterEssencesData)
        {
            _characterEssencesData = characterEssencesData;
        }

        public void Destroy()
        {
            _characterEssencesData.ClearAll();
        }
    }
}
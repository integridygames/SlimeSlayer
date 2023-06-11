using Game.Gameplay.Models.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Essence
{
    public class CharacterEssenceDestroySystem : IDestroySystem
    {
        private readonly GameResourceData _gameResourceData;

        public CharacterEssenceDestroySystem(GameResourceData gameResourceData)
        {
            _gameResourceData = gameResourceData;
        }

        public void Destroy()
        {
            _gameResourceData.ClearAll();
        }
    }
}
using Game.Gameplay.Services;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character
{
    public class CharacterSpawnSystem : IInitializeSystem
    {
        private readonly CharacterRespawnService _characterRespawnService;

        public CharacterSpawnSystem(CharacterRespawnService characterRespawnService)
        {
            _characterRespawnService = characterRespawnService;
        }

        public void Initialize()
        {
            _characterRespawnService.GoToSpawnPoint();
        }
    }
}
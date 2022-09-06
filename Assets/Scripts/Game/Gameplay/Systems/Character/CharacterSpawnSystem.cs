using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character
{
    public class CharacterSpawnSystem : IInitializeSystem
    {
        private readonly LevelInfo _levelInfo;
        private readonly CharacterView _characterView;

        public CharacterSpawnSystem(LevelInfo levelInfo, CharacterView characterView)
        {
            _levelInfo = levelInfo;
            _characterView = characterView;
        }

        public void Initialize()
        {
            _characterView.Rigidbody.MovePosition(_levelInfo.CurrentLevelView.Value.SpawnPointView.transform.position);
        }
    }
}
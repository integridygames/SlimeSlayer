using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class CharacterLevelController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterLevelController(GameScreenView controlledEntity,
            CharacterCharacteristicsRepository characterCharacteristicsRepository) : base(controlledEntity)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }


        public void Initialize()
        {
            _characterCharacteristicsRepository.CurrentLevelProgress.OnUpdate += OnCurrentLevelProgressUpdateHandler;
        }

        public void Dispose()
        {
            _characterCharacteristicsRepository.CurrentLevelProgress.OnUpdate += OnCurrentLevelProgressUpdateHandler;
        }

        private void OnCurrentLevelProgressUpdateHandler(RxValue<float> rxValue)
        {
           ControlledEntity.LevelProgressBar.SetProgress(rxValue.NewValue);
        }
    }
}
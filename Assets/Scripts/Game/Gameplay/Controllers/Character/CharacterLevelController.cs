using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.UI.Screens;
using Game.Gameplay.Views.UI.Screens.Gameplay;
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
            _characterCharacteristicsRepository.CurrentLevel.OnUpdate += OnCurrentLevelUpdateHandler;
        }

        public void Dispose()
        {
            _characterCharacteristicsRepository.CurrentLevelProgress.OnUpdate -= OnCurrentLevelProgressUpdateHandler;
            _characterCharacteristicsRepository.CurrentLevel.OnUpdate -= OnCurrentLevelUpdateHandler;
        }

        private void OnCurrentLevelProgressUpdateHandler(RxValue<float> rxValue)
        {
           ControlledEntity.LevelProgressBar.SetProgress(rxValue.NewValue);
        }

        private void OnCurrentLevelUpdateHandler(RxValue<int> rxValue)
        {
            ControlledEntity.Level.text = $"Lv. {rxValue.NewValue}";
        }
    }
}
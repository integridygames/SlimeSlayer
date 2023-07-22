using System;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.UI.Abilities;
using TegridyCore;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Abilities
{
    public class ChooseAbilityController : ControllerBase<ChooseAbilityView>, IInitializable, IDisposable
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly AbilitiesDistributorService _abilitiesDistributorService;
        private readonly AbilitiesRepository _abilitiesRepository;

        public ChooseAbilityController(ChooseAbilityView controlledEntity,
            CharacterCharacteristicsRepository characterCharacteristicsRepository,
            AbilitiesDistributorService abilitiesDistributorService,
            AbilitiesRepository abilitiesRepository) : base(controlledEntity)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _abilitiesDistributorService = abilitiesDistributorService;
            _abilitiesRepository = abilitiesRepository;
        }

        public void Initialize()
        {
            _characterCharacteristicsRepository.CurrentLevelProgress.OnUpdate += OnCurrentLevelProgressUpdateHandler;
            ControlledEntity.OnAbilitySelected += OnAbilitySelectedHandler;
        }

        public void Dispose()
        {
            _characterCharacteristicsRepository.CurrentLevelProgress.OnUpdate -= OnCurrentLevelProgressUpdateHandler;
            ControlledEntity.OnAbilitySelected -= OnAbilitySelectedHandler;
        }

        private void OnCurrentLevelProgressUpdateHandler(RxValue<float> _)
        {
            if (_characterCharacteristicsRepository.ReadyForLevelUp == false)
            {
                return;
            }

            Time.timeScale = 0;
            ControlledEntity.gameObject.SetActive(true);

            var abilitiesForNextLevel = _abilitiesDistributorService.GetAbilitiesForNextLevel();

            for (int i = 0; i < AbilitiesDistributorService.MaxAbilitiesForLevel; i++)
            {
                var abilityView = ControlledEntity.AbilityViews[i];

                if (i >= abilitiesForNextLevel.Count)
                {
                    abilityView.gameObject.SetActive(false);
                    continue;
                }

                abilityView.gameObject.SetActive(true);
                var abilityRecord = abilitiesForNextLevel[i];

                abilityView.SetAbilityType(abilityRecord.AbilityType);
                abilityView.SetName(abilityRecord.Name);
                abilityView.SetDescription(abilityRecord.Description);
                abilityView.SetIcon(abilityRecord.AbilitySprite);

                var level = 1;

                if (_abilitiesRepository.ActiveAbilitiesDict.TryGetValue(abilityRecord.AbilityType, out var ability))
                {
                    ability.Level++;
                    level = ability.Level;
                }

                var abilityLevelRecord = abilityRecord.GetInfoForLevel(level);

                abilityView.SetLevel(level);
                abilityView.SetWholeEffect(abilityLevelRecord._wholeEffect);
            }
        }

        private void OnAbilitySelectedHandler(AbilityType abilityType)
        {
            Time.timeScale = 1;
            ControlledEntity.gameObject.SetActive(false);

            _characterCharacteristicsRepository.LevelUp();
        }
    }
}
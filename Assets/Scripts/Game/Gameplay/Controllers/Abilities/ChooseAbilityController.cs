using System;
using Game.DataBase.Abilities;
using Game.Gameplay.Factories;
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
        private readonly AbilityFactory _abilityFactory;

        public ChooseAbilityController(ChooseAbilityView controlledEntity,
            CharacterCharacteristicsRepository characterCharacteristicsRepository,
            AbilitiesDistributorService abilitiesDistributorService,
            AbilitiesRepository abilitiesRepository, AbilityFactory abilityFactory) : base(controlledEntity)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _abilitiesDistributorService = abilitiesDistributorService;
            _abilitiesRepository = abilitiesRepository;
            _abilityFactory = abilityFactory;
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

                var level = 1;

                if (_abilitiesRepository.ActiveAbilitiesDict.TryGetValue(abilityRecord.AbilityType, out var ability))
                {
                    level = ability.Level + 1;
                }

                abilityView.SetAbilityData(abilityRecord, level);
            }
        }



        private void OnAbilitySelectedHandler(AbilityRecord abilityRecord)
        {
            Time.timeScale = 1;
            ControlledEntity.gameObject.SetActive(false);

            if (_abilitiesRepository.ActiveAbilitiesDict.TryGetValue(abilityRecord.AbilityType, out var ability) == false)
            {
                ability = _abilityFactory.CreateAbility(abilityRecord.AbilityType);
            }

            ability.Level++;
            _characterCharacteristicsRepository.LevelUpAbility(abilityRecord, ability.Level);

            if (ability.Level == 1)
            {
                _abilitiesRepository.AddAbility(abilityRecord.AbilityType, ability);
            }

            _characterCharacteristicsRepository.LevelUp();
        }
    }
}
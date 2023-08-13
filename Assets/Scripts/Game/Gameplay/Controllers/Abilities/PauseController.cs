using System;
using System.Collections.Generic;
using Game.DataBase.Abilities;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Abilities
{
    public class PauseController : ControllerBase<PauseView>, IInitializable, IDisposable
    {
        private readonly AbilitiesRepository _abilitiesRepository;
        private readonly AbilitiesDataBase _abilitiesDataBase;

        public PauseController(PauseView controlledEntity, AbilitiesRepository abilitiesRepository, AbilitiesDataBase abilitiesDataBase) : base(controlledEntity)
        {
            _abilitiesRepository = abilitiesRepository;
            _abilitiesDataBase = abilitiesDataBase;
        }

        public void Initialize()
        {
            ControlledEntity.OnCloseButtonPressed += OnCloseButtonPressedHandler;
            ControlledEntity.OnShow += OnShowHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnCloseButtonPressed -= OnCloseButtonPressedHandler;
            ControlledEntity.OnShow -= OnShowHandler;
        }

        private void OnCloseButtonPressedHandler()
        {
            ControlledEntity.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnShowHandler()
        {
            var abilitiesData = new List<(AbilityRecord record, int level)>(_abilitiesRepository.ActiveAbilities.Count);
            foreach (var abilityType in _abilitiesRepository.ActiveAbilitiesSet)
            {
                (AbilityRecord record, int level) abilityData = new();

                var abilityBase = _abilitiesRepository.ActiveAbilitiesDict[abilityType];

                var abilityRecord = _abilitiesDataBase.GetRecordByType(abilityType);

                abilityData.level = abilityBase.Level;
                abilityData.record = abilityRecord;

                abilitiesData.Add(abilityData);
            }

            ControlledEntity.SetAbilities(abilitiesData);
        }
    }
}
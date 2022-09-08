using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class CharacterHealthController : ControllerBase<CharacterHealthView>, IInitializable, IDisposable
    {
        private readonly CharacterHealthData _characterHealthData;
        private readonly CharacterStats _characterStats;

        public CharacterHealthController(CharacterHealthView controlledEntity, CharacterHealthData characterHealthData,
            CharacterStats characterStats) : base(controlledEntity)
        {
            _characterHealthData = characterHealthData;
            _characterStats = characterStats;
        }

        public void Initialize()
        {
            _characterHealthData.CurrentHealth.OnUpdate += CurrentHealthOnOnUpdate;
        }

        public void Dispose()
        {
            _characterHealthData.CurrentHealth.OnUpdate -= CurrentHealthOnOnUpdate;
        }

        private void CurrentHealthOnOnUpdate(RxValue<int> rxValue)
        {
            ControlledEntity.SetHealthPercentage((float) rxValue.NewValue / _characterStats.MaxHealth);
        }
    }
}
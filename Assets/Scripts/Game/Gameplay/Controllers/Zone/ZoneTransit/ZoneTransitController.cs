using Game.Gameplay.Models.Zone;
using System;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Zone.ZoneTransit 
{
    public class ZoneTransitController : ControllerBase<ZoneTransitInfo>, IInitializable, IDisposable
    {
        public ZoneTransitController(ZoneTransitInfo zoneTransitInfo) : base(zoneTransitInfo) { }

        public void Initialize()
        {
            ControlledEntity.OnCharacterEnteredTrigger += OnCharacterEnteredTriggerHandler;
            ControlledEntity.OnCharacterLeftTrigger += OnCharacterLeftTriggerHandler;
            ControlledEntity.OnOpenButtonClicked += OnOpenButtonClickedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnCharacterEnteredTrigger -= OnCharacterEnteredTriggerHandler;
            ControlledEntity.OnCharacterLeftTrigger -= OnCharacterLeftTriggerHandler;
            ControlledEntity.OnOpenButtonClicked -= OnOpenButtonClickedHandler;
        }

        private void OnCharacterEnteredTriggerHandler() 
        {
            ControlledEntity.SetTriggerNearestZoneTransitState(true);
        }

        private void OnCharacterLeftTriggerHandler() 
        {
            ControlledEntity.SetTriggerNearestZoneTransitState(false);
            ControlledEntity.SetOpeningButtonState(false);
        }

        private void OnOpenButtonClickedHandler() 
        {
            ControlledEntity.SetOpeningButtonState(true);
        }
    }
}
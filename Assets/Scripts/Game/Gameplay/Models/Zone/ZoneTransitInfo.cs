using Game.Gameplay.Views.Zone;
using System;

namespace Game.Gameplay.Models.Zone 
{
    public class ZoneTransitInfo
    {
        public event Action OnCharacterEnteredTrigger;
        public event Action OnCharacterLeftTrigger;
        public event Action OnOpenButtonClicked;

        public ZoneTransitMenuView ZoneTransitMenuView { get; private set; }
        public ZoneTransitView NearestZoneTransitView { get; private set; }

        public bool WasButtonClicked { get; private set; }
        public bool IsCharacterInNearestZoneTrigger { get; private set; }

        public void Initialize(ZoneTransitMenuView zoneTransitMenuView)
        { 
            ZoneTransitMenuView = zoneTransitMenuView;
            WasButtonClicked = false;
            IsCharacterInNearestZoneTrigger = false;
            ZoneTransitMenuView.OpenButton.onClick.AddListener(OnClick);
        }

        public void SetNearestZoneTransit(ZoneTransitView nearestZoneTransitView)
        {        
            if(NearestZoneTransitView != null) 
            {
                NearestZoneTransitView.OnCharacterEntered -= OnCharacterEnteredTrigger;
                NearestZoneTransitView.OnCharacterLeft -= OnCharacterLeftTrigger;
            }

            NearestZoneTransitView = nearestZoneTransitView;
            WasButtonClicked = false;
            IsCharacterInNearestZoneTrigger = false;

            if(NearestZoneTransitView != null) 
            {
                NearestZoneTransitView.OnCharacterEntered += OnCharacterEnteredTrigger;
                NearestZoneTransitView.OnCharacterLeft += OnCharacterLeftTrigger;
            }
        }

        public void SetOpeningButtonState(bool state) 
        {
            WasButtonClicked = state;
        }

        public void SetTriggerNearestZoneTransitState(bool state) 
        {
            IsCharacterInNearestZoneTrigger = state;
        }

        private void OnClick()
        {
            OnOpenButtonClicked?.Invoke();
        }
    }
}
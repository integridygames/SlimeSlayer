using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTrigger : ViewBase
    {
        [SerializeField] private ZoneView _zoneView;
        [SerializeField] private bool _isEntranceZone;
        [SerializeField] private bool _isHubTrigger;

        private ZonesInfo _zonesInfo;

        [Inject]
        public void Construct(ZonesInfo zonesInfo) 
        {
            _zonesInfo = zonesInfo;
        }

        private void OnTriggerEnter(Collider body)
        {
            if (body.TryGetComponent<CharacterView>(out CharacterView characterView)) 
            {              
                _zoneView.ChangeZoneState(_isEntranceZone);
                TryToSetCurrentZoneToNull();
                _isEntranceZone = !_isEntranceZone;
            }
        }

        private void TryToSetCurrentZoneToNull() 
        {
            if (CheckIfHub()) 
            {
                _zonesInfo.SetCurrentZone(null);
            }
        }

        private bool CheckIfHub() 
        {
            return _isHubTrigger && !_isEntranceZone;
        }
    }
}
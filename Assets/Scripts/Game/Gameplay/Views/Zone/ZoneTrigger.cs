using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneTrigger : ViewBase
    {
        [SerializeField] private ZoneView _zoneView;
        [SerializeField] private bool _isEntranceZone;
        [SerializeField] private bool _isHubTrigger;

        private ZonesInfo _zonesInfo;

        public void Initialize(ZonesInfo zonesInfo) 
        {
            _zonesInfo = zonesInfo;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterView>(out CharacterView characterView))
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
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Zone 
{
    public class CurrentZoneCatchSystem : IUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;

        public CurrentZoneCatchSystem(ZonesInfo zonesInfo)
        {
            _zonesInfo = zonesInfo;
        }

        public void Update()
        {
            foreach (ZoneView zone in _zonesInfo.Zones) 
            {
                if (CheckIfZoneTriggered(zone))                                                  
                    _zonesInfo.SetCurrentZone(zone);                
            }
        }

        private bool CheckIfZoneTriggered(ZoneView zone) 
        {
            return zone.IsZoneTriggered && zone != _zonesInfo.CurrentZone;
        }
    }
}
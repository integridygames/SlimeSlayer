using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Zone 
{
    public class ZoneTriggersInitializeSystem : IInitializeSystem
    {
        private readonly ZonesInfo _zonesInfo;

        public ZoneTriggersInitializeSystem(ZonesInfo zonesInfo) 
        {
            _zonesInfo = zonesInfo;
        }

        public void Initialize()
        {
            foreach(ZoneView zone in _zonesInfo.Zones) 
            {
                foreach(ZoneTrigger trigger in zone.ZoneTriggers) 
                {
                    trigger.Initialize(_zonesInfo);
                }
            }
        }
    }
}
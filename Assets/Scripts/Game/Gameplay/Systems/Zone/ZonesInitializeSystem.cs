using System.Collections.Generic;
using TegridyCore.Base;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Models.Zone;
using System.Linq;
using Game.Gameplay.Models.Level;
using JetBrains.Annotations;

namespace Game.Gameplay.Systems.Zone 
{
    [UsedImplicitly]
    public class ZonesInitializeSystem : IInitializeSystem
    {
        private readonly ZonesInfo _zonesInfo;
        private readonly LevelInfo _levelInfo;
        private List<ZoneView> _zones;

        public ZonesInitializeSystem(ZonesInfo zonesInfo, LevelInfo levelInfo) 
        {
            _zonesInfo = zonesInfo;
            _levelInfo = levelInfo;           
        }

        public void Initialize()
        {
            _zones = _levelInfo.CurrentLevelView.Value.ZonesViews.ToList();
            _zonesInfo.InitializeZones(_zones);
            _zonesInfo.SetCurrentZone(null);

            foreach (var zone in _zonesInfo.Zones) 
            {
                zone.Initialize();
            }
        }
    }
}
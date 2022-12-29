using Game.Gameplay.Controllers.Level;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Zone 
{
    public class ZonesReinitializingController : ControllerBase<LevelInfo>, IInitializable, IDisposable
    {
        private const int MinimumZonesCount = 3;
        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly LevelDestroyingController _levelDestroyingController;

        public ZonesReinitializingController(ZonesDataContainer zonesDataContainer, LevelInfo levelInfo,
            LevelDestroyingController levelDestroyingController) : base(levelInfo) 
        {
            _zonesDataContainer = zonesDataContainer;
            _levelDestroyingController = levelDestroyingController;
        }

        public void Initialize()
        {
            _levelDestroyingController.LevelWasDestroyed += ReinitializeZones;
        }

        public void Dispose()
        {
            _levelDestroyingController.LevelWasDestroyed -= ReinitializeZones;
        }

        public void ReinitializeZones()
        {
            _zonesDataContainer.Clear();
            var zoneViews = ControlledEntity.CurrentLevelView.Value.ZonesViews.ToList();

            var zonesData = GetFilledZonesData(zoneViews);

            _zonesDataContainer.InitializeZonesData(zonesData);
            _zonesDataContainer.SetCurrentZone(zonesData[0]);
        }

        private static List<ZoneData> GetFilledZonesData(List<ZoneView> zoneViews)
        {
            var zonesData = new List<ZoneData>(MinimumZonesCount);

            foreach (var zone in zoneViews)
            {              
                var zoneData = zone is BattlefieldZoneView ? new BattlefieldZoneData(zone) : new ZoneData(zone);

                if (zoneData is BattlefieldZoneData battlefieldZoneData)
                    battlefieldZoneData.Recycle();

                zonesData.Add(zoneData);
            }

            return zonesData;
        }      
    }
}
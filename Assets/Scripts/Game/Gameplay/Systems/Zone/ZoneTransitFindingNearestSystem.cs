using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone 
{
    public class ZoneTransitFindingNearestSystem : IUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private readonly CharacterView _characterView;
        private readonly GameScreenView _gameScrennView;

        public ZoneTransitFindingNearestSystem(ZonesInfo zonesInfo, ZoneTransitInfo zoneTransitInfo, CharacterView characterView, GameScreenView gameScrennView) 
        {
            _zonesInfo = zonesInfo;
            _zoneTransitInfo = zoneTransitInfo;
            _characterView = characterView;
            _gameScrennView = gameScrennView;
        }

        public void Update()
        {
            if (Condition()) 
            {
                FindNearestTransit();
            }
        }

        private void FindNearestTransit() 
        {
            float nearestZoneTransitDistance = float.MaxValue;
            ZoneTransitView nearestZoneTransitView = null;

            if(ConditionIfZoneTransitsNotNull()) 
            {
                foreach(var zoneTransit in _zonesInfo.CurrentZoneData.ZoneView.ZoneTransits) 
                {
                    if(ConditionForNearestZoneTransit(zoneTransit, nearestZoneTransitDistance, out nearestZoneTransitDistance)) 
                    {
                        nearestZoneTransitView = zoneTransit;
                    }
                }                
            }

            if(nearestZoneTransitView != _zoneTransitInfo.NearestZoneTransitView)
                _zoneTransitInfo.SetNearestZoneTransit(nearestZoneTransitView);
        }

        private bool ConditionIfZoneTransitsNotNull() 
        {
            return _zonesInfo.CurrentZoneData.ZoneView.ZoneTransits != null;
        }

        private bool ConditionForNearestZoneTransit(ZoneTransitView zoneTransit, float nearestZoneTransitDistance, out float currentNearestDistance) 
        {
            float distance = Vector3.Distance(zoneTransit.transform.position, _characterView.transform.position);
            currentNearestDistance = nearestZoneTransitDistance;

            if (distance < nearestZoneTransitDistance) 
            {
                currentNearestDistance = distance;
                return true;
            }

            return false;
        }

        private bool Condition() 
        {
            return _gameScrennView.gameObject.activeInHierarchy && _zonesInfo.CurrentZoneData.ZoneView.ZoneTransits.Length > 0;
        }
    }
}
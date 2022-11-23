using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone.ZoneTransit
{
    public class ZoneTransitFindingNearestSystem : IUpdateSystem
    {
        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private readonly CharacterView _characterView;

        public ZoneTransitFindingNearestSystem(ZonesDataContainer zonesDataContainer, ZoneTransitInfo zoneTransitInfo,
            CharacterView characterView)
        {
            _zonesDataContainer = zonesDataContainer;
            _zoneTransitInfo = zoneTransitInfo;
            _characterView = characterView;
        }

        public void Update()
        {
            FindNearestTransit();
        }

        private void FindNearestTransit()
        {
            var nearestZoneTransitDistance = float.MaxValue;
            ZoneTransitView nearestZoneTransitView = null;

            foreach (var zoneTransit in _zonesDataContainer.CurrentZoneData.ZoneTransitViews)
            {
                if (ConditionForNearestZoneTransit(zoneTransit, nearestZoneTransitDistance,
                        out nearestZoneTransitDistance))
                {
                    nearestZoneTransitView = zoneTransit;
                }
            }

            if (nearestZoneTransitView != _zoneTransitInfo.NearestZoneTransitView)
                _zoneTransitInfo.SetNearestZoneTransit(nearestZoneTransitView);
        }

        private bool ConditionForNearestZoneTransit(ZoneTransitView zoneTransit, float nearestZoneTransitDistance,
            out float currentNearestDistance)
        {
            currentNearestDistance = nearestZoneTransitDistance;

            if (!zoneTransit.IsOpened)
            {
                var distance = Vector3.Distance(zoneTransit.transform.position, _characterView.transform.position);

                if (distance < nearestZoneTransitDistance)
                {
                    currentNearestDistance = distance;
                    return true;
                }
            }

            return false;
        }
    }
}
using Game.Gameplay.Utils.Zones;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class ZoneView : ViewBase
    {
        [SerializeField] private Vector2 _zoneSize;
        [SerializeField] private ZoneType _zoneType;
        [SerializeField] private ZoneTriggerView[] _zoneTriggers;
        [SerializeField] private ZoneTransitView[] _zoneTransits;

        public ZoneType ZoneType => _zoneType;
        public ZoneTriggerView[] ZoneTriggers => _zoneTriggers;
        public ZoneTransitView[] ZoneTransits => _zoneTransits;
        public Vector2 ZoneSize => _zoneSize;
    }
}
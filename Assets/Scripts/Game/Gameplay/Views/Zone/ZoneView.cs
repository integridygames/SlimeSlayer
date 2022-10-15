using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class ZoneView : ViewBase
    {
        [SerializeField] private Vector2 _zoneSize;
        [SerializeField] private ZoneType _zoneType;
        [SerializeField] private ZoneTransitView[] _zoneTransits;

        public ZoneType ZoneType => _zoneType;
        public ZoneTransitView[] ZoneTransits => _zoneTransits;
        public Vector2 ZoneSize => _zoneSize;
    }
}
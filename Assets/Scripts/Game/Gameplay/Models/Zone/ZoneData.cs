using Game.Gameplay.Views.Zone;
using UnityEngine;

namespace Game.Gameplay.Models.Zone
{
    public class ZoneData
    {
        public ZoneTransitView[] ZoneTransitViews => _zoneView.ZoneTransitView;

        public Vector3 Position => _zoneView.transform.position;
        public Vector3 Size => _zoneView.ZoneSize;

        private readonly ZoneView _zoneView;

        public ZoneData(ZoneView zoneView)
        {
            _zoneView = zoneView;
        }
    }
}
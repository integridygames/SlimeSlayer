using Game.Gameplay.Views.Zone;
using UnityEngine;

namespace Game.Gameplay.Models.Zone
{
    public class ZoneData
    {
        public readonly int ZoneId;

        private static int _currentId;

        public ZoneTransitView[] ZoneTransitViews => _zoneView.ZoneTransitView;

        public Vector3 Position => _zoneView.transform.position;
        public Vector3 Size => _zoneView.ZoneSize;

        private readonly ZoneView _zoneView;

        private Bounds _boundsOfZone;

        public ZoneData(ZoneView zoneView)
        {
            _zoneView = zoneView;
            ZoneId = _currentId++;

            var zoneSize = zoneView.ZoneSize;
            zoneSize.y = 10;

            _boundsOfZone = new Bounds(zoneView.transform.position, zoneSize);
        }

        public bool InBoundsOfZone(Vector3 position)
        {
            return _boundsOfZone.Contains(position);
        }
    }
}
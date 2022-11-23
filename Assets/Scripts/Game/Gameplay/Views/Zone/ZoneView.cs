using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    [RequireComponent(typeof(MeshFilter))]
    public class ZoneView : ViewBase
    {
        private ZoneTransitView[] _zoneTransitView;
        public ZoneTransitView[] ZoneTransitView => _zoneTransitView ??= GetComponentsInChildren<ZoneTransitView>();

        private Vector3? _zoneSize;
        public Vector3 ZoneSize => _zoneSize ??= CalculateZoneSize();

        private Vector3 CalculateZoneSize()
        {
            return Vector3.Scale(GetComponent<MeshFilter>().mesh.bounds.size, transform.localScale);
        }
    }
}
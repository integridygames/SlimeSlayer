using Game.Gameplay.Views.Zone;

namespace Game.Gameplay.Models.Zone 
{
    public class ZoneTransitInfo
    {
        public ZoneTransitMenuView ZoneTransitMenuView { get; private set; }
        public ZoneTransitView NearestZoneTransitView { get; private set; }

        public void InitializeZoneTransitMenu(ZoneTransitMenuView zoneTransitMenuView)
        {
            ZoneTransitMenuView = zoneTransitMenuView;
        }

        public void SetNearestZoneTransit(ZoneTransitView nearestZoneTransitView)
        {
            NearestZoneTransitView = nearestZoneTransitView;
        }
    }
}
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Zone.ZoneTransit 
{
    public class ZoneTransitInitializeSystem : IInitializeSystem
    {
        private readonly ZoneTransitMenuView _zoneTransitMenuView;
        private readonly ZoneTransitInfo _zoneTransitInfo;

        public ZoneTransitInitializeSystem(ZoneTransitMenuView zoneTransitMenuView, ZoneTransitInfo zoneTransitInfo) 
        {
            _zoneTransitMenuView = zoneTransitMenuView;
            _zoneTransitInfo = zoneTransitInfo;
        }

        public void Initialize()
        {
            _zoneTransitInfo.Initialize(_zoneTransitMenuView);
            _zoneTransitMenuView.Initialzie();

            _zoneTransitMenuView.gameObject.SetActive(false);
        }
    }
}
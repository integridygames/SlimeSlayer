using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using TegridyCore.Base;

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
        _zoneTransitInfo.InitializeZoneTransitMenu(_zoneTransitMenuView);
        _zoneTransitMenuView.gameObject.SetActive(false);
    }
}

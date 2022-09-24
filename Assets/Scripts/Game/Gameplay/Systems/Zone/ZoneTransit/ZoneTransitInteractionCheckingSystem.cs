using Game.Gameplay.Models.Zone;
using TegridyCore.Base;

public class ZoneTransitInteractionCheckingSystem : IUpdateSystem
{
    private readonly ZoneTransitInfo _zoneTransitInfo;
    private readonly ZoneTransitInteractionInfo _zoneTransitInteractionInfo;

    public ZoneTransitInteractionCheckingSystem(ZoneTransitInfo zoneTransitInfo, ZoneTransitInteractionInfo zoneTransitInteractionInfo) 
    {
        _zoneTransitInfo = zoneTransitInfo;
        _zoneTransitInteractionInfo = zoneTransitInteractionInfo;
    }

    public void Update()
    {
        if (Condition())
        {
            _zoneTransitInteractionInfo.AllowOpeningProcess();
            _zoneTransitInteractionInfo.SetCurrentEssencesDataset(_zoneTransitInfo.NearestZoneTransitView.EssenceData);
        }
        else
        {
            _zoneTransitInteractionInfo.DisallowOpeningProcess();
            _zoneTransitInteractionInfo.SetCurrentEssencesDataset(new ZoneTransitEssenceData[0]);
            _zoneTransitInteractionInfo.ChangeElapsedTime(0);       
        }
    }

    private bool Condition() 
    {
        return _zoneTransitInfo.IsCharacterInNearestZoneTrigger && _zoneTransitInfo.WasButtonClicked;
    }
}
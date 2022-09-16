using TegridyCore.Base;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Views.SampleScene.Screens;

namespace Game.Gameplay.Systems.Zone 
{
    public class ZoneTransitMenuChangingVisualSystem : IUpdateSystem
    {
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private ZoneTransitView _previousZoneTransitView;
        private readonly GameScreenView _gameScrennView;

        public ZoneTransitMenuChangingVisualSystem(ZoneTransitInfo zoneTransitInfo, GameScreenView gameScrennView) 
        {
            _zoneTransitInfo = zoneTransitInfo;
            _previousZoneTransitView = _zoneTransitInfo.NearestZoneTransitView;
            _gameScrennView = gameScrennView;
        }

        public void Update()
        {
            if (Condition()) 
            {
                _previousZoneTransitView = _zoneTransitInfo.NearestZoneTransitView;
                SetVisual();
            }
        }

        private bool Condition() 
        {
            return _gameScrennView.gameObject.activeInHierarchy && 
                   _zoneTransitInfo.NearestZoneTransitView != _previousZoneTransitView &&
                   _zoneTransitInfo.NearestZoneTransitView != null;
        }

        private void SetVisual() 
        {
            for(int i = 0; i < _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews.Length; i++) 
            {
                if(i < _zoneTransitInfo.NearestZoneTransitView.EssenceData.Length) 
                {
                    _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews[i].gameObject.SetActive(true);

                    _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews[i].QuantityTMPText.text =
                         _zoneTransitInfo.NearestZoneTransitView.EssenceData[i].Quantity.ToString();

                    _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews[i].EssenceImage.color =
                         _zoneTransitInfo.NearestZoneTransitView.EssenceData[i].EssenceColor;
                }
                else
                {
                    _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
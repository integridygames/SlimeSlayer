using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.SampleScene.Screens;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone 
{
    public class ZoneTransitMenuPositioningSystem : IUpdateSystem
    {
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private readonly GameScreenView _gameScreenView;
        private readonly CameraContainerView _cameraContainerView;
        private readonly CharacterView _characterView;

        private const float MinDistanceToPlayer = 20f;

        public ZoneTransitMenuPositioningSystem(ZoneTransitInfo zoneTransitInfo, GameScreenView gameScreenView, CameraContainerView cameraContainerView,
             CharacterView characterView) 
        {
            _zoneTransitInfo = zoneTransitInfo;
            _gameScreenView = gameScreenView;
            _cameraContainerView = cameraContainerView;
            _characterView = characterView;
        }

        public void Update()
        {
            if (_gameScreenView.gameObject.activeInHierarchy && _zoneTransitInfo.NearestZoneTransitView != null &&
                Vector3.Distance(_characterView.transform.position, _zoneTransitInfo.NearestZoneTransitView.transform.position) <= MinDistanceToPlayer)
            {
                if(!_zoneTransitInfo.ZoneTransitMenuView.gameObject.activeInHierarchy)
                     _zoneTransitInfo.ZoneTransitMenuView.gameObject.SetActive(true);

                var screenPosition = _cameraContainerView.Camera.WorldToScreenPoint(_zoneTransitInfo.NearestZoneTransitView.transform.position);
                _zoneTransitInfo.ZoneTransitMenuView.transform.position = screenPosition;
            }
            else if (_zoneTransitInfo.ZoneTransitMenuView.gameObject.activeInHierarchy)
                _zoneTransitInfo.ZoneTransitMenuView.gameObject.SetActive(false);
        }      
    }  
}
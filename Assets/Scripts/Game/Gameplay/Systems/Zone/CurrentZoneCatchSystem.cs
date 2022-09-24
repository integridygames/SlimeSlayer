using TegridyCore.Base;
using Game.Gameplay.Models.Zone;
using UnityEngine;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Systems.Zone 
{
    public class CurrentZoneCatchSystem : IUpdateSystem
    {
        private readonly ZonesInfo _zonesInfo;
        private readonly CharacterView _characterView;

        public CurrentZoneCatchSystem(ZonesInfo zonesInfo, CharacterView characterView) 
        {
            _zonesInfo = zonesInfo;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach(var zoneData in _zonesInfo.ZonesDatas) 
                if(Condition(zoneData)) 
                    _zonesInfo.SetCurrentZone(zoneData);
        }

        public bool Condition(ZoneData zoneData) 
        {
            (Vector3, Vector3) rectangleCoordinates = DetermineRectangleCoordinates(zoneData);

            return _zonesInfo.CurrentZoneData != zoneData &&
                   _characterView.transform.position.x >= rectangleCoordinates.Item1.x &&
                   _characterView.transform.position.x <= rectangleCoordinates.Item2.x &&
                   _characterView.transform.position.z >= rectangleCoordinates.Item2.z &&
                   _characterView.transform.position.z <= rectangleCoordinates.Item1.z;
        }

        private (Vector3, Vector3) DetermineRectangleCoordinates(ZoneData zoneData) 
        {           
            Vector3 leftUpperCorner = CalculateReactanleCoordinates(zoneData, false, true);
            Vector3 rightBottomCorner = CalculateReactanleCoordinates(zoneData, true, false);

            return (leftUpperCorner, rightBottomCorner);
        }

        private Vector3 CalculateReactanleCoordinates(ZoneData zoneData, bool isAdditionX, bool isAdditionZ) 
        {
            float leftUpperCornerX = CalculateRectangleCoordinate(zoneData.ZoneView.transform.position.x, zoneData.ZoneView.ZoneSize.x, isAdditionX);
            float leftUpperCornerZ = CalculateRectangleCoordinate(zoneData.ZoneView.transform.position.z, zoneData.ZoneView.ZoneSize.y, isAdditionZ);

            return new Vector3(leftUpperCornerX, zoneData.ZoneView.transform.position.y, leftUpperCornerZ);
        }

        private float CalculateRectangleCoordinate(float zonePosition, float zoneSize, bool isAddition) 
        {
            switch (isAddition) 
            {
                case true:
                    return zonePosition + zoneSize / 2;
                case false:
                    return zonePosition - zoneSize / 2;
            }
          
        }
    }
}
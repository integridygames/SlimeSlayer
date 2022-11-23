using TegridyCore.Base;
using Game.Gameplay.Models.Zone;
using UnityEngine;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Systems.Zone 
{
    public class CurrentZoneCatchSystem : IUpdateSystem
    {
        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly CharacterView _characterView;

        public CurrentZoneCatchSystem(ZonesDataContainer zonesDataContainer, CharacterView characterView)
        {
            _zonesDataContainer = zonesDataContainer;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach(var zoneData in _zonesDataContainer.ZonesData)
                if(Condition(zoneData)) 
                    _zonesDataContainer.SetCurrentZone(zoneData);
        }

        public bool Condition(ZoneData zoneData) 
        {
            var rectangleCoordinates = DetermineRectangleCoordinates(zoneData);

            var characterPosition = _characterView.transform.position;

            return _zonesDataContainer.CurrentZoneData != zoneData &&
                   characterPosition.x >= rectangleCoordinates.Item1.x &&
                   characterPosition.x <= rectangleCoordinates.Item2.x &&
                   characterPosition.z >= rectangleCoordinates.Item2.z &&
                   characterPosition.z <= rectangleCoordinates.Item1.z;
        }

        private (Vector3, Vector3) DetermineRectangleCoordinates(ZoneData zoneData) 
        {           
            var leftUpperCorner = CalculateRectangleCoordinates(zoneData, false, true);
            var rightBottomCorner = CalculateRectangleCoordinates(zoneData, true, false);

            return (leftUpperCorner, rightBottomCorner);
        }

        private Vector3 CalculateRectangleCoordinates(ZoneData zoneData, bool isAdditionX, bool isAdditionZ)
        {
            var leftUpperCornerX = CalculateRectangleCoordinate(zoneData.Position.x, zoneData.Size.x, isAdditionX);
            var leftUpperCornerZ = CalculateRectangleCoordinate(zoneData.Position.z, zoneData.Size.y, isAdditionZ);

            return new Vector3(leftUpperCornerX, zoneData.Position.y, leftUpperCornerZ);
        }

        private float CalculateRectangleCoordinate(float zonePosition, float zoneSize, bool isAddition)
        {
            return isAddition switch
            {
                true => zonePosition + zoneSize / 2,
                false => zonePosition - zoneSize / 2
            };
        }
    }
}
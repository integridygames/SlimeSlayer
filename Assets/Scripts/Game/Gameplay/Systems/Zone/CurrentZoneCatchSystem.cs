using TegridyCore.Base;
using Game.Gameplay.Models.Zone;
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
            foreach(var zoneData in _zonesDataContainer.ZonesData.Values)
                if(zoneData.InBoundsOfZone(_characterView.transform.position))
                    _zonesDataContainer.SetCurrentZone(zoneData);
        }
    }
}
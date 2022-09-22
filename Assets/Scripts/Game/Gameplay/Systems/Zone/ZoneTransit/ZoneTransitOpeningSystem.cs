using Game.Gameplay.Models.Zone;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Zone.ZoneTransit 
{
    public class ZoneTransitOpeningSystem : IUpdateSystem
    {
        private readonly ZoneTransitInfo _zoneTransitInfo;
    
        public ZoneTransitOpeningSystem(ZoneTransitInfo zoneTransitInfo) 
        {
            _zoneTransitInfo = zoneTransitInfo;    
        }     

        public void Update()
        {           
            if(Condition())
            {
                bool isEveryEssenceQunatityZero = CheckIfEveryEssenceQuantityIsZero();

                if (isEveryEssenceQunatityZero)               
                    Open();                              
            }
        }

        private bool CheckIfEveryEssenceQuantityIsZero() 
        {
            bool isEveryEssenceQunatityZero = true;
            foreach (var essenceData in _zoneTransitInfo.NearestZoneTransitView.EssenceData)
            {
                if (essenceData.Quantity > 0)
                    isEveryEssenceQunatityZero = false;
            }

            return isEveryEssenceQunatityZero;
        }

        private void Open() 
        {
            _zoneTransitInfo.NearestZoneTransitView.Open();
            _zoneTransitInfo.NearestZoneTransitView.gameObject.SetActive(false);
        }

        private bool Condition() 
        {
            return _zoneTransitInfo.NearestZoneTransitView != null;
        }
    }
}
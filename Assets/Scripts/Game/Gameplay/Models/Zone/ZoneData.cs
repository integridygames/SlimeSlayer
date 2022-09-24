using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;
using System.Collections.Generic;
using System.Linq;

namespace Game.Gameplay.Models.Zone 
{
    public class ZoneData
    {
        public ZoneView ZoneView { get; private set; }
        public List<EnemyView> EnemiesPool { get; private set; }
        public bool IsZoneTriggered { get; private set; }

        public ZoneData(ZoneView zone) 
        {
            ZoneView = zone;
            IsZoneTriggered = false;
        }

        public void Initialize()
        {
            IsZoneTriggered = false;
            EnemiesPool = ZoneView.GetComponentsInChildren<EnemyView>().ToList();
        }

        public void ClearPool()
        {   
            EnemiesPool.Clear();
        }
    }  
}
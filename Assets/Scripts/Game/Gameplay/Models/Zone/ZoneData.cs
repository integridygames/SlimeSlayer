using Game.Gameplay.Utils.Zones;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public void ChangeZoneState(bool state)
        {
            if (ZoneView.ZoneType != ZoneType.Hub)
                IsZoneTriggered = state;
        }

        public Vector2 GetRandomPoint()
        {
            float randomX = Random.Range(-ZoneView.ZoneSize.x / 2, ZoneView.ZoneSize.x / 2);
            float randomZ = Random.Range(-ZoneView.ZoneSize.y / 2, ZoneView.ZoneSize.y / 2);

            return new Vector2(ZoneView.transform.position.x + randomX, ZoneView.transform.position.z + randomZ);
        }
    }  
}
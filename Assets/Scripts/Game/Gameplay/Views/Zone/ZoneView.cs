using Game.Gameplay.Views.Enemy;
using System.Collections.Generic;
using System.Linq;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone 
{
    public class ZoneView : ViewBase
    {
        [SerializeField] private Vector2 zoneSize;

        public List<ZoneTrigger> ZoneTriggers { get; private set; }
        public List<EnemyView> EnemiesPool { get; private set; }

        public bool IsZoneTriggered { get; private set; } = false;      

        public void Initialize()
        {
            EnemiesPool = new List<EnemyView>();
            EnemiesPool = GetComponentsInChildren<EnemyView>().ToList();
            ZoneTriggers = GetComponentsInChildren<ZoneTrigger>().ToList();
        }

        public void ClearPool() 
        {
            EnemiesPool.Clear();
        }

        public void ChangeZoneState(bool state) 
        {
            IsZoneTriggered = state;
        }

        public Vector2 GetRandomPoint() 
        {
            float randomX = Random.Range(-zoneSize.x / 2, zoneSize.x / 2);
            float randomZ = Random.Range(-zoneSize.y / 2, zoneSize.y / 2);

            return new Vector2(transform.position.x + randomX, transform.position.z + randomZ);
        }
    } 
}
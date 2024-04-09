using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.DataBase.Weapon;
using NaughtyAttributes;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Loot
{
    [CreateAssetMenu(fileName = "LootDataBase", menuName = "ScriptableObjects/LootDataBase")]
    public class LootDataBase : ScriptableObject
    {
        [SerializeField, ArrayWithKey] private List<LevelLootTable> levelLootTables;

        public List<PlayerWeaponData> GetLootForLevel(LevelType levelType)
        {
            var loot = new List<PlayerWeaponData>();
            var levelLootTable = levelLootTables.Find(table => table.LevelType == levelType);

            if (levelLootTable == null) return loot;

            var lootItems = levelLootTable.GenerateLoot();
            loot.AddRange(lootItems.Select(lootItem => new PlayerWeaponData(lootItem.weaponType, lootItem.rarityType)));

            return loot;
        }

        [Button]
        public void TestLootSystem()
        {
            var playerWeaponDatas = GetLootForLevel(LevelType.LevelWithDemons);

            var stringBuilder = new StringBuilder();
            
            foreach (var playerWeaponData in playerWeaponDatas)
            {
                stringBuilder.AppendLine($"WeaponType: {playerWeaponData._weaponType}, RarityType: {playerWeaponData._rarityType}");
            }

            Debug.Log(stringBuilder.ToString());
        }
    }

    [Serializable]
    public class LevelLootTable
    {
        [SerializeField, ArrayKey] private LevelType levelType;
        [SerializeField] private List<LootItem> lootItems;

        [SerializeField] private int minLootCount;
        [SerializeField] private int maxLootCount;
        [SerializeField] private float lootCountDecreaseFactor;

        public LevelType LevelType => levelType;
        public List<LootItem> LootItems => lootItems;

        public List<LootItem> GenerateLoot()
        {
            var loot = new List<LootItem>();
            var lootCount = GetRandomLootCount();

            var droppedItemsCount = new Dictionary<LootItem, int>();

            for (var i = 0; i < lootCount; i++)
            {
                loot.Add(GetRandomLootItem(droppedItemsCount));
            }

            return loot;
        }

        private int GetRandomLootCount()
        {
            float totalChance = 0;
            for (var i = minLootCount; i <= maxLootCount; i++)
            {
                totalChance += 1 / Mathf.Pow(i, lootCountDecreaseFactor);
            }

            var randomValue = UnityEngine.Random.Range(0, totalChance);
            var cumulative = 0f;

            for (var i = minLootCount; i <= maxLootCount; i++)
            {
                cumulative += 1 / Mathf.Pow(i, lootCountDecreaseFactor);
                if (randomValue < cumulative)
                {
                    return i;
                }
            }

            return minLootCount;
        }

        private LootItem GetRandomLootItem(Dictionary<LootItem, int> droppedItemsCount)
        {
            var lootTotalChance = LootItems.Sum(item =>
            {
                if (droppedItemsCount.TryGetValue(item, out var count))
                {
                    return item.dropChance / count;
                }

                return item.dropChance;
            });

            var randomValue = UnityEngine.Random.Range(0, lootTotalChance);
            var cumulative = 0f;

            foreach (var lootItem in LootItems)
            {
                droppedItemsCount.TryAdd(lootItem, 1);

                var dropChance = lootItem.dropChance / droppedItemsCount[lootItem];

                cumulative += dropChance;
                if (randomValue < cumulative)
                {
                    droppedItemsCount[lootItem]++;

                    return lootItem;
                }
            }

            return null;
        }

        [Serializable]
        public class LootItem
        {
            public WeaponType weaponType;
            public RarityType rarityType;
            public float dropChance;
        }
    }
}
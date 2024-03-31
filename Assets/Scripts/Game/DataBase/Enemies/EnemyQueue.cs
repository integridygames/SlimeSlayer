using System;
using System.Collections.Generic;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    /// <summary>
    /// Очередь врагов для спавна.
    /// Каждый элемент очереди - группа врагов одного типа.
    /// Группы очереди спавнятся последовательно друг за другом.
    /// Следующая группа начинает спавниться только после смерти последнего врага в группе.
    /// </summary>
    [Serializable]
    public class EnemyQueue
    {
        [SerializeField, ArrayWithKey]
        private List<EnemyGroupSpawnSettings> _enemyGroupsSettings = new();

        public List<EnemyGroupSpawnSettings> EnemyGroupsSettings => _enemyGroupsSettings;
    }
}
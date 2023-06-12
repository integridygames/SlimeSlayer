using System.Linq;
using Game.DataBase.GameResource;
using UnityEngine;

namespace Game.DataBase.Essence
{
    [CreateAssetMenu(fileName = "GameResourcesDataBase", menuName = "ScriptableObjects/GameResourcesDataBase")]
    public class GameResourcesDataBase : AbstractDataBase<GameResourceRecord, GameResourceType>
    {
        public override GameResourceRecord GetRecordByType(GameResourceType recordType)
        {
            return Records.First(x => x._gameResourceType == recordType);
        }
    }
}
using System.Linq;
using UnityEngine;

namespace Game.DataBase.Character
{
    [CreateAssetMenu(fileName = "CharacterCharacteristicsDataBase", menuName = "ScriptableObjects/CharacterCharacteristicsDataBase")]
    public class CharacterCharacteristicsDataBase : AbstractDataBase<CharacterCharacteristicRecord, CharacterCharacteristicType>
    {
        public override CharacterCharacteristicRecord GetRecordByType(CharacterCharacteristicType recordType)
        {
            return Records.First(x => x._characterCharacteristicType == recordType);
        }
    }
}
using System;

namespace Game.DataBase.Character
{
    [Serializable]
    public class PlayerCharacteristicData
    {
        public CharacterCharacteristicType _characterCharacteristicType;
        public bool _hidden;
        public int _level;

        public PlayerCharacteristicData(CharacterCharacteristicType characteristicType, bool hidden)
        {
            _characterCharacteristicType = characteristicType;
            _hidden = hidden;
            _level = 1;
        }
    }
}
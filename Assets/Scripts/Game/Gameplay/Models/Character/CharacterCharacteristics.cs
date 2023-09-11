using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Character;
using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterCharacteristics
    {
        public event Action OnUpdate;

        private readonly CharacterCharacteristicsDataBase _characteristicsDataBase;
        private readonly ApplicationData _applicationData;

        private readonly Dictionary<CharacterCharacteristicType, (float, float)>
            _characterCharacteristics = new();

        private readonly Dictionary<CharacterCharacteristicType, PlayerCharacteristicData> _characteristicDataCache =
            new();

        public CharacterCharacteristics(CharacterCharacteristicsDataBase characteristicsDataBase,
            ApplicationData applicationData)
        {
            _characteristicsDataBase = characteristicsDataBase;
            _applicationData = applicationData;
        }

        public Sprite GetIcon(PlayerCharacteristicData playerCharacteristicData)
        {
            var characteristicRecord =
                _characteristicsDataBase.GetRecordByType(playerCharacteristicData._characterCharacteristicType);

            return characteristicRecord._icon;
        }

        public int GetPrice(PlayerCharacteristicData playerCharacteristicData)
        {
            if (_characterCharacteristics.ContainsKey(playerCharacteristicData._characterCharacteristicType) == false)
            {
                UpdateCharacteristic(playerCharacteristicData);
            }

            return (int) _characterCharacteristics[playerCharacteristicData._characterCharacteristicType].Item2;
        }

        public float GetCharacteristic(CharacterCharacteristicType characteristicType)
        {
            if (_characteristicDataCache.TryGetValue(characteristicType, out var playerCharacteristicData) == false)
            {
                playerCharacteristicData = _applicationData.PlayerData.PlayerCharacteristicsData.First(x =>
                    x._characterCharacteristicType == characteristicType);

                _characteristicDataCache[characteristicType] = playerCharacteristicData;
            }

            return GetCharacteristic(playerCharacteristicData);
        }

        public float GetCharacteristic(PlayerCharacteristicData playerCharacteristicData)
        {
            if (_characterCharacteristics.ContainsKey(playerCharacteristicData._characterCharacteristicType) == false)
            {
                UpdateCharacteristic(playerCharacteristicData);
            }

            return _characterCharacteristics[playerCharacteristicData._characterCharacteristicType].Item1;
        }

        public void UpdateCharacteristic(PlayerCharacteristicData playerCharacteristicData)
        {
            var characteristicRecord =
                _characteristicsDataBase.GetRecordByType(playerCharacteristicData._characterCharacteristicType);

            var characteristicValueAndPrice =
                CalculateCharacteristicValueAndPrice(characteristicRecord, playerCharacteristicData._level);

            SetCharacteristic(playerCharacteristicData._characterCharacteristicType, characteristicValueAndPrice.Item1,
                characteristicValueAndPrice.Item2);
        }

        private (float, float) CalculateCharacteristicValueAndPrice(CharacterCharacteristicRecord characteristicRecord,
            int level)
        {
            var characteristicValue = characteristicRecord._startValue;

            characteristicValue += GetCharacteristicAddition(characteristicRecord._addition,
                characteristicRecord._additionMultiplier, level);

            var characteristicPrice = characteristicRecord._startPrice;

            characteristicPrice += GetCharacteristicAddition(characteristicRecord._priceAddition,
                characteristicRecord._priceAdditionMultiplier, level);

            return (characteristicValue, characteristicPrice);
        }


        private void SetCharacteristic(CharacterCharacteristicType characteristicType, float value, float price)
        {
            _characterCharacteristics[characteristicType] = (value, price);

            OnUpdate?.Invoke();
        }

        public float GetCharacteristicAddition(float addition, float multiplier, int level)
        {
            float result = 0;

            for (var i = 1; i < level; i++)
            {
                result += addition * multiplier;
            }

            return result;
        }
    }
}
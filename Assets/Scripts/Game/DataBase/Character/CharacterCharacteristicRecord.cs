using System;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Character
{
    [Serializable]
    public class CharacterCharacteristicRecord
    {
        [ArrayKey]
        public CharacterCharacteristicType _characterCharacteristicType;

        public float _startValue;

        public float _addition;
        public float _additionMultiplier;

        public float _startPrice;
        public float _priceAddition;
        public float _priceAdditionMultiplier;

        [Tooltip("Hidden from player. For technical usage.")]
        public bool _hidden;

        public Sprite _icon;
    }
}
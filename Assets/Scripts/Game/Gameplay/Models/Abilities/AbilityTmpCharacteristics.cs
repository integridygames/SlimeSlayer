using System.Collections.Generic;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilityTmpCharacteristics
    {
        private Dictionary<AbilityTmpCharacteristicType, float> _characteristicValues = new();

        public float GetCharacteristic(AbilityTmpCharacteristicType characteristicType)
        {
            if (_characteristicValues.TryGetValue(characteristicType, out var value)) ;

            return value;
        }

        public void SetCharacteristic(AbilityTmpCharacteristicType characteristicType, float value)
        {
            _characteristicValues[characteristicType] = value;
        }
    }
}
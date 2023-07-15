using System.Collections.Generic;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilityTmpCharacteristics
    {
        private readonly Dictionary<AbilityType, float> _characteristicValues = new();

        public float GetCharacteristic(AbilityType characteristicType)
        {
            if (_characteristicValues.TryGetValue(characteristicType, out var value)) ;

            return value;
        }

        public void SetCharacteristic(AbilityType characteristicType, float value)
        {
            _characteristicValues[characteristicType] = value;
        }
    }
}
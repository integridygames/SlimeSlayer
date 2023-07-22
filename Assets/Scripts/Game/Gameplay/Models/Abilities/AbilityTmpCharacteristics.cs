using System.Collections.Generic;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilityTmpCharacteristics
    {
        private readonly Dictionary<AbilityCharacteristicType, float> _characteristicValues = new();

        public bool TryGetCharacteristic(AbilityCharacteristicType characteristicType, out float value)
        {
            return _characteristicValues.TryGetValue(characteristicType, out value);
        }

        public void SetCharacteristic(AbilityCharacteristicType characteristicType, float value)
        {
            _characteristicValues[characteristicType] = value;
        }

        public void Clear()
        {
            _characteristicValues.Clear();
        }
    }
}
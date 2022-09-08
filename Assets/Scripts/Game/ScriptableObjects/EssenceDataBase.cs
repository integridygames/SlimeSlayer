using Game.Gameplay.Utils.Essences;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "EssenceDataBase", menuName = "ScriptableObjects/EssenceDataBase")]
    public class EssenceDataBase : ScriptableObject
    {
        [SerializeField] private EssenceRecord[] _essences;

        public EssenceRecord[] Essences => _essences;

        public EssenceRecord GetEssenceRecordByIndex(int index)
        {
            if (index < _essences.Length)
            {
                return _essences[index];
            }

            return _essences.GetRandomElement();
        }

        public EssenceRecord GetEssenceRecordByType(EssenceType essenceType)
        {
            foreach (var essence in _essences)
            {
                if (essence.EssenceType == essenceType)
                {
                    return essence;
                }
            }

            return _essences.GetRandomElement();
        }
    }
}
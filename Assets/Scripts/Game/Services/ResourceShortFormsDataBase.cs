using System;
using System.Linq;
using UnityEngine;

namespace Game.Services
{
    [CreateAssetMenu(fileName = "ResourceShortFormsDataBase", menuName = "ScriptableObjects/ResourceShortFormsDataBase")]
    public class ResourceShortFormsDataBase : ScriptableObject
    {
        [SerializeField] private ResourceShortForm[] _resourceShortForms;

        public string GetCurrentForm(int coinsCount)
        {
            var sortedResourceForms = _resourceShortForms.OrderBy(x => x._coinsCount);

            foreach (var resourceShortForm in sortedResourceForms)
            {
                if (coinsCount >= resourceShortForm._coinsCount)
                {
                    var convertedCount = (float)resourceShortForm._coinsCount;

                    return $"{coinsCount / convertedCount:0.0}{resourceShortForm._character}";
                }
            }

            return coinsCount.ToString();
        }

        [Serializable]
        private struct ResourceShortForm
        {
            public int _coinsCount;
            public string _character;
        }
    }
}
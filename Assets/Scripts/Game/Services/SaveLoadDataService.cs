using JetBrains.Annotations;
using UnityEngine;

namespace Game.Services
{
    [UsedImplicitly]
    public static class SaveLoadDataService
    {
        public static void Save<T>(T data)
        {
            var serializedData = JsonUtility.ToJson(data);

            var key = typeof(T).Name;

            PlayerPrefs.SetString(key, serializedData);
        }

        public static T Load<T>(T defaultValue)
        {
            var key = typeof(T).Name;

            var serializedData = PlayerPrefs.GetString(key);

            if (string.IsNullOrEmpty(serializedData))
                return defaultValue;

            return JsonUtility.FromJson<T>(serializedData);
        }
    }
}
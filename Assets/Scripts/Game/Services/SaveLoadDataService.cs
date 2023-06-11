using System.IO;
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

            var path = BuildPath(key);

            using var fileStream = new StreamWriter(path);
            fileStream.Write(serializedData);
        }

        public static T Load<T>(T defaultValue)
        {
            var key = typeof(T).Name;

            var path = BuildPath(key);

            using var fileStream = new StreamReader(path);

            var serializedData = fileStream.ReadToEnd();

            if (string.IsNullOrEmpty(serializedData))
                return defaultValue;

            return JsonUtility.FromJson<T>(serializedData);
        }

        private static string BuildPath(string key)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, key + ".dat");

            if (File.Exists(fullPath) == false)
            {
                File.Create(fullPath);
            }

            return fullPath;
        }
    }
}
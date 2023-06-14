using UnityEditor;
using UnityEngine;

namespace TegridyUtils.Editor
{
    public static class MenuItems
    {
        [MenuItem("Custom Tools/Remove Local AppData")]
        public static void RemoveLocalAppData()
        {
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        }
    }
}
using Game.Gameplay.Views.Level;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsDataBase", menuName = "ScriptableObjects/LevelsDataBase")]
    public class LevelsDataBase : ScriptableObject
    {
        [SerializeField] private LevelView[] _levelViews;

        public LevelView GetLevelPrefabByIndex(int index)
        {
            if (index < _levelViews.Length)
            {
                return _levelViews[index];
            }

            return _levelViews.GetRandomElement();
        }
    }
}
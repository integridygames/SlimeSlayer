using UnityEngine;
using Game.Gameplay.Views.Bullets;
using TegridyUtils.Extensions;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "AmmoDataBase", menuName = "ScriptableObjects/AmmoDataBase")]
    public class AmmoDataBase : ScriptableObject
    {
        [SerializeField] private AmmoView[] _ammos;

        public AmmoView GetAmmoPrefabByIndex(int index)
        {
            if (index < _ammos.Length)
            {
                return _ammos[index];
            }

            return _ammos.GetRandomElement();
        }

        public AmmoView GetAmmoPrefabByID(string ID)
        {
            foreach (var ammo in _ammos)
            {
                if (ammo.ID == ID)
                {
                    return ammo;
                }
            }

            return _ammos.GetRandomElement();
        }
        
    }
}
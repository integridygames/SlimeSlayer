using UnityEngine;
using Game.ScriptableObjects.Substructure;
using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : PrefabsDataBase<WeaponRecord, WeaponType, BulletView>
    {
       
    }
}